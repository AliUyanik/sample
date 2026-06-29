var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseCors(policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

var displayMessage = builder.Configuration["AppSettings:DisplayMessage"]
    ?? "AppSettings:DisplayMessage tanımlı değil.";

app.MapGet("/api/config", () => new { displayMessage });

app.Run();
