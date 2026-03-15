using Microsoft.EntityFrameworkCore;
using WorkItemTrackerApi.Data;
using WorkItemTrackerApi.DTOs;
using WorkItemTrackerApi.Models;

public class WorkItemServiceIntegrationTests : IDisposable
{
    private readonly AppDbContext _context;
    private readonly WorkItemService _workItemService;

    public WorkItemServiceIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("Data Source=:memory:")
            .Options;

        _context = new AppDbContext(options);
        _context.Database.OpenConnection();      // keep in-memory db alive
        _context.Database.EnsureCreated();       // applies schema

        var repository = new WorkItemRepository(_context);
        _workItemService = new WorkItemService(repository);
    }

    [Fact]
    public async Task CreateWorkItem_PersistsToDatabase()
    {
        var workItemDto = new WorkItemDto { Title = "Fix bug", Description = "Details" };

        var result = await _workItemService.CreateWorkItem(workItemDto);

        var workItem = await _context.WorkItems.FindAsync(result.Id);

        Assert.NotNull(workItem);
        Assert.Equal("Fix bug", workItem.Title);
        Assert.Equal("Open", workItem.Status);
    }

    [Fact]
    public async Task GetWorkItems_ReturnsAllPersistedItems()
    {
        _context.WorkItems.AddRange(
            new WorkItem { Title = "Task 1" },
            new WorkItem { Title = "Task 2" }
        );
        await _context.SaveChangesAsync();

        var result = await _workItemService.GetWorkItems();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetWorkItem_ExistingId_ReturnsCorrectItem()
    {
        var item = new WorkItem { Title = "Task A" };
        _context.WorkItems.Add(item);
        await _context.SaveChangesAsync();

        var result = await _workItemService.GetWorkItem(item.Id);

        Assert.NotNull(result);
        Assert.Equal("Task A", result.Title);
    }

    [Fact]
    public async Task GetWorkItem_NonExistingId_ReturnsNull()
    {
        var result = await _workItemService.GetWorkItem(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateWorkItem_PersistsChanges()
    {
        var item = new WorkItem { Title = "Old Title", Status = "Open" };
        _context.WorkItems.Add(item);
        await _context.SaveChangesAsync();

        item.Title = "New Title";
        item.Status = "In Progress";
        var result = await _workItemService.UpdateWorkItem(item);

        var fromDb = await _context.WorkItems.FindAsync(item.Id);
        Assert.Equal("New Title", fromDb!.Title);
        Assert.Equal("In Progress", fromDb.Status);
    }

    [Fact]
    public async Task UpdateWorkItem_NonExistingId_ThrowsKeyNotFoundException()
    {
        var ghost = new WorkItem { Id = 999, Title = "Ghost" };

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _workItemService.UpdateWorkItem(ghost));
    }

    [Fact]
    public async Task DeleteWorkItem_ExistingId_RemovesItem()
    {
        var item = new WorkItem { Title = "Task to delete" };
        _context.WorkItems.Add(item);
        await _context.SaveChangesAsync();

        await _workItemService.DeleteWorkItem(item.Id);

        var fromDb = await _context.WorkItems.FindAsync(item.Id);
        Assert.Null(fromDb);
    }

    [Fact]
    public async Task SearchWorkItem_ReturnsMatchingItems()
    {
        _context.WorkItems.AddRange(
            new WorkItem { Title = "Fix login bug" },
            new WorkItem { Title = "Update documentation" },
            new WorkItem { Title = "Login page redesign" }
        );
        await _context.SaveChangesAsync();

        var results = await _workItemService.SearchWorkItem("Login");

        Assert.Equal(2, results.Count);
    }

    public void Dispose()
    {
        _context.Database.CloseConnection();
        _context.Dispose();
    }
}