namespace Reece.TaskTracker.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public string Status { get; set; } = "pending";
}

