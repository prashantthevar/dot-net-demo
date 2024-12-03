using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Retrieve the DATABASE_URL from the environment variables
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

if (string.IsNullOrEmpty(databaseUrl))
{
    throw new InvalidOperationException("Environment variable 'DATABASE_URL' is not set.");
}

// Parse DATABASE_URL into a MySQL connection string
var connectionString = ConvertDatabaseUrlToConnectionString(databaseUrl);

// Add DbContext with the parsed connection string
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


// Helper function to convert DATABASE_URL to MySQL connection string
string ConvertDatabaseUrlToConnectionString(string databaseUrl)
{
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');
    return $"Server={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};User={userInfo[0]};Password={userInfo[1]};";
}