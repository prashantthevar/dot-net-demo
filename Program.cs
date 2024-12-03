using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Retrieve the MySQL connection string from the environment variable
var connectionString = builder.Configuration.GetValue<string>("DATABASE_URL");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DATABASE_URL' is not set.");
}

// Use the MySQL provider for Entity Framework Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

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
