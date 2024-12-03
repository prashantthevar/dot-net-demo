var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => new { Message = "Hello, Railway!", Timestamp = DateTime.UtcNow });

app.MapGet("/user", () => new 
{
    Id = 1,
    Name = "Jane Doe",
    Email = "jane.doe@example.com",
    Role = "Developer"
});

app.Run();