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
}