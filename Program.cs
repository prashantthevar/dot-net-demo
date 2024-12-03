var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Use the environment variable PORT if available, otherwise fallback to 5000
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.MapGet("/", () => "Hello, World!");

app.Run($"http://0.0.0.0:{port}");
