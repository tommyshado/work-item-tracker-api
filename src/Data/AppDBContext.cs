using Microsoft.EntityFrameworkCore;
using WorkItemTrackerApi.Models;

namespace WorkItemTrackerApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<WorkItem> WorkItems => Set<WorkItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkItem>()
            .Property(w => w.Status)
            .HasConversion<string>();
    }
}