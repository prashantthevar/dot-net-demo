var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => new { Message = "Hello, World!", Timestamp = DateTime.UtcNow });

app.MapGet("/user", () => new 
{
    Id = 1,
    Name = "John Doe",
    Email = "john.doe@example.com",
    Role = "Admin"
});

app.Run();

app.Run();
