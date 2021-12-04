using BursaryFinderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BursaryFinderAPI.Context;

public class BursaryFinderContext : DbContext
{
    public BursaryFinderContext(DbContextOptions<BursaryFinderContext> contextOptions) : base(contextOptions) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Organization> Organization { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=bursaryfinder;Username=bursaryfinder;Password=bursaryfinder");

    public override int SaveChanges()
    {
        ChangeTracker.DetectChanges();
        return base.SaveChanges();
    }
}