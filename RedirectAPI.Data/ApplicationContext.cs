using Microsoft.EntityFrameworkCore;

namespace RedirectAPI.Data;

public class ApplicationContext : DbContext
{
    
    public ApplicationContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Link> Links { get; set; } = null!;
}