using Microsoft.EntityFrameworkCore;
using RedirectAPI.Data.DataEntity;

namespace RedirectAPI.Data;

public class ApplicationContext : DbContext
{
    
    public ApplicationContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");
        base.OnModelCreating(modelBuilder);
    }


    /// Database table
    public DbSet<User> Users { get; set; } = null!;
    /// Database table
    public DbSet<Link> Links { get; set; } = null!;
    /// Database table
    public DbSet<Image> Images { get; set; } = null!;
}