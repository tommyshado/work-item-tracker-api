using Microsoft.EntityFrameworkCore;
using WorkItemTrackerApi.Data;
using WorkItemTrackerApi.Models;

public class WorkItemRepository : IWorkItemRepository
{
    private readonly AppDbContext _context;

    public WorkItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<WorkItem>> GetAll()
    {
        try
        {
            return await _context.WorkItems
                .OrderByDescending(workItem => workItem.UpdatedAt)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching work items: {ex.Message}");
            throw;
        }
    }

    public async Task<WorkItem?> GetById(int id)
    {
        try
        {
            return await _context.WorkItems.FindAsync(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching work item with ID {id}: {ex.Message}");
            throw;
        }
    }

    public async Task<WorkItem> Create(WorkItem item)
    {
        try
        {
            _context.WorkItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating work item: {ex.Message}");
            throw;
        }
    }

    public async Task<WorkItem> Update(WorkItem item)
    {
        try
        {
            var existingItem = await _context.WorkItems.FindAsync(item.Id);
            if (existingItem == null)
            {
                throw new KeyNotFoundException($"WorkItem with ID {item.Id} not found.");
            }

            existingItem.Title = item.Title;
            existingItem.Description = item.Description;
            existingItem.Status = item.Status;
            existingItem.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingItem;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating work item with ID {item.Id}: {ex.Message}");
            throw;
        }
    }

    public async Task Delete(int id)
    {
        try
        {
            var existingItem = await _context.WorkItems.FindAsync(id);
            if (existingItem == null)
            {
                throw new KeyNotFoundException($"WorkItem with ID {id} not found.");
            }

            _context.WorkItems.Remove(existingItem);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting work item with ID {id}: {ex.Message}");
            throw;
        }
    }

    public async Task<List<WorkItem>> Search(string query)
    {
        try
        {
            var results = await _context.WorkItems
                .Where(w => w.Title.ToLower().Contains(query.ToLower()) || w.Description.ToLower().Contains(query.ToLower()))
                .OrderByDescending(w => w.UpdatedAt)
                .ToListAsync();
            return results;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error searching work items with query '{query}': {ex.Message}");
            throw;
        }
    }

    public async Task<List<WorkItem>> GetByStatus(WorkItemStatus status)
    {
        try
        {
            return await _context.WorkItems
                        .Where(w => w.Status == status)
                        .OrderByDescending(w => w.UpdatedAt)
                        .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching work items with status '{status}': {ex.Message}");
            throw;
        }
    }

    public async Task<List<WorkItem>> GetWorkItemByTime(int timeframe)
    {
        try
        {
            var now = DateTime.UtcNow;
            var fromDate = now.AddMinutes(-timeframe);
            return await _context.WorkItems
                    .Where(w => w.UpdatedAt >= fromDate && w.UpdatedAt <= now)
                    .OrderByDescending(w => w.UpdatedAt)
                    .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching work items by time frame '{timeframe}': {ex.Message}");
            throw;
        }
    }

}