var builder = WebApplication.CreateBuilder(args);

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
app.MapGet("/api/greeting", () => Results.Ok(new { message = "Merhaba dünya" }));

app.Run();

