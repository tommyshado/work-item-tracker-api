using WorkItemTrackerApi.DTOs;
using WorkItemTrackerApi.Models;

public class WorkItemService : IWorkItemService
{
    private readonly IWorkItemRepository _repository;

    public WorkItemService(IWorkItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<WorkItem>> GetWorkItems()
    {
        return await _repository.GetAll();
    }

    public async Task<WorkItem?> GetWorkItem(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<WorkItem> CreateWorkItem(WorkItemDto dto)
    {
        var workItem = new WorkItem
        {
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return await _repository.Create(workItem);
    }

    public async Task<WorkItem> UpdateWorkItem(WorkItem item)
    {
        return await _repository.Update(item);
    }

    public async Task DeleteWorkItem(int id)
    {
        await _repository.Delete(id);
    }

    public async Task<List<WorkItem>> SearchWorkItem(string query)
    {
        return await _repository.Search(query);
    }

    public async Task<List<WorkItem>> GetWorkItemsByStatus(WorkItemStatus status)
    {
        return await _repository.GetByStatus(status);
    }

    public async Task<List<WorkItem>> GetWorkItemsByTime(int timeframe)
    {
        return await _repository.GetWorkItemByTime(timeframe);
    }
}