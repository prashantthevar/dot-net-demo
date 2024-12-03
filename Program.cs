using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// var connectionString = builder.Configuration.GetValue<string>("RAILWAY_DATABASE_URL");
var connectionString = "mysql://root:MYKbAUaIbRlyurGUSXhyQqsDyuoviKVz@autorack.proxy.rlwy.net:49127/railway";


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
