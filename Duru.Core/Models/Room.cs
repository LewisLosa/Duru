namespace Duru.Core.Models;

public class Room
{
    public int RoomId { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public int? RoomTypeId { get; set; }
    public string? Status { get; set; }
    public int? Floor { get; set; }
}