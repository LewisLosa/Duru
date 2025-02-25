namespace Duru.Data.Entities;

public class Room : IEntity
{
    public int Id { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public int? RoomTypeId { get; set; }
    public string? Status { get; set; }
    public int? Floor { get; set; }
}