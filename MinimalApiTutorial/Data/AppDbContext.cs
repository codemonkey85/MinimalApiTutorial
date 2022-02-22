namespace MinimalApiTutorial.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Todo>()
            .Property(b => b.IsCompleted)
            .HasDefaultValue(false);
    }

    public DbSet<Todo> Todos { get; set; }
}
