using WorkItemTrackerApi.Models;

public interface IWorkItemRepository
{
    Task<List<WorkItem>> GetAll();
    Task<WorkItem?> GetById(int id);
    Task<WorkItem> Create(WorkItem item);
    Task<WorkItem> Update(WorkItem item);
    Task Delete(int id);
    Task<List<WorkItem>> Search(string query);
    Task<List<WorkItem>> GetByStatus(WorkItemStatus status);
}