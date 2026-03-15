using WorkItemTrackerApi.DTOs;
using WorkItemTrackerApi.Models;

public interface IWorkItemService
{
    Task<List<WorkItem>> GetWorkItems();
    Task<WorkItem?> GetWorkItem(int id);
    Task<WorkItem> CreateWorkItem(WorkItemDto dto);
    Task<WorkItem> UpdateWorkItem(WorkItem item);
}