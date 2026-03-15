namespace WorkItemTrackerApi.Models;

public enum WorkItemStatus
{
    Open,
    Closed,
    InProgress,
    Blocked
}

public class WorkItem
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public WorkItemStatus Status { get; set; } = WorkItemStatus.Open;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
