namespace WorkItemTrackerApi.DTOs;

using WorkItemTrackerApi.Models;

public class WorkItemDto
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public WorkItemStatus Status { get; set; } = WorkItemStatus.Open;
}
