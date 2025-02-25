namespace Duru.Data.Entities;

public class RoomType : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? Capacity { get; set; }
    public double? Price { get; set; }
}