namespace Duru.Core.Models;

public class MaintenanceRequests
{
    public int RequestId { get; set; }
    public int RoomId { get; set; }
    public int EmployeeId { get; set; }
    public string? Description { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}