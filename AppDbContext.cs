using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Todo> Todos { get; set; }
}

public class Todo
{
    public int Id { get; set; }
    public string Task { get; set; }
    public bool IsCompleted { get; set; }
}
