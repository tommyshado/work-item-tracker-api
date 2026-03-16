using WorkItemTrackerApi.DTOs;
using WorkItemTrackerApi.Models;

public interface IWorkItemService
{
    Task<List<WorkItem>> GetWorkItems();
    Task<WorkItem?> GetWorkItem(int id);
    Task<WorkItem> CreateWorkItem(WorkItemDto dto);
    Task<WorkItem> UpdateWorkItem(WorkItem item);
    Task DeleteWorkItem(int id);
    Task<List<WorkItem>> SearchWorkItem(string query);
    Task<List<WorkItem>> GetWorkItemsByStatus(WorkItemStatus status);
    Task<List<WorkItem>> GetWorkItemsByTime(int timeframe);
}