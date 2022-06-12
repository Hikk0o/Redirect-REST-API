using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace RedirectAPI.Data;

public class ApplicationContext : DbContext
{
    
    public ApplicationContext(DbContextOptions options)
        : base(options)
    {
        // Console.WriteLine(options.BuildOptionsFragment());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Link> Links { get; set; } = null!;
}