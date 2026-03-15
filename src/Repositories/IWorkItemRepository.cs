using WorkItemTrackerApi.Models;

public interface IWorkItemRepository
{
    Task<List<WorkItem>> GetAll();
    Task<WorkItem?> GetById(int id);
    Task<WorkItem> Create(WorkItem item);
    Task<WorkItem> Update(WorkItem item);
}