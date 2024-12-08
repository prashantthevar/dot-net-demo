using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Server=autorack.proxy.rlwy.net;Port=49127;Database=railway;Uid=root;Pwd=MYKbAUaIbRlyurGUSXhyQqsDyuoviKVz;";

// Register DbContext with dependency injection container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();


// Use the environment variable PORT if available, otherwise fallback to 5000
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.MapGet("/", () => "Hello, World!");

// Example minimal API endpoints interacting with the database
app.MapGet("/todos", async (AppDbContext db) =>
{
    return await db.Todos.ToListAsync();
});

app.MapPost("/todos", async (AppDbContext db, Todo todo) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todos/{todo.Id}", todo);
});

app.Run($"http://0.0.0.0:{port}");