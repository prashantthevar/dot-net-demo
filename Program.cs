var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello, Railway!");
app.MapGet("/user", () => new { Id = 1, Name = "Jane Doe" });

app.Run();
