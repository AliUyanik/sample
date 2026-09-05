using RabbitMQ.Client;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

var redisConnectionString = builder.Configuration.GetConnectionString("Redis")
    ?? throw new InvalidOperationException("ConnectionStrings:Redis is required.");
var rabbitMqUri = builder.Configuration["RabbitMq:Uri"]
    ?? throw new InvalidOperationException("RabbitMq:Uri is required.");
var googleBaseUrl = builder.Configuration["RemoteApis:Google:BaseUrl"]
    ?? throw new InvalidOperationException("RemoteApis:Google:BaseUrl is required.");

// Connections are created only when a consumer requests them from DI, so the API
// can start even when the local Redis or RabbitMQ services are not running yet.
builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
    ConnectionMultiplexer.Connect(redisConnectionString));
builder.Services.AddSingleton<IConnectionFactory>(_ => new ConnectionFactory
{
    Uri = new Uri(rabbitMqUri)
});

builder.Services.AddHttpClient("Google", client =>
{
    client.BaseAddress = new Uri(googleBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(10);
    client.DefaultRequestHeaders.UserAgent.ParseAdd("TestProject.Api/1.0");
});

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

app.UseCors();

var displayMessage = (
    builder.Configuration["AppSettings:DisplayMessage"]
    ?? "AppSettings:DisplayMessage tanımlı değil.").Trim();

app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));
app.MapGet("/api/config", () => new { displayMessage });
app.MapGet("/api/remote/google", async (
    IHttpClientFactory httpClientFactory,
    CancellationToken cancellationToken) =>
{
    using var response = await httpClientFactory
        .CreateClient("Google")
        .GetAsync(string.Empty, cancellationToken);

    return Results.Ok(new
    {
        statusCode = (int)response.StatusCode,
        response.IsSuccessStatusCode,
        target = response.RequestMessage?.RequestUri?.ToString()
    });
});

app.Run();

