namespace WorkItemTrackerApi.Models;

// Status should be Open or Closed types
public static class WorkItemStatus
{
    public const string Open = "Open";
    public const string Closed = "Closed";
}

public class WorkItem
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Status { get; set; } = WorkItemStatus.Open;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
