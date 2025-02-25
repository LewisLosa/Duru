namespace Duru.Data.Entities;

public class Service : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public double? Price { get; set; }
    public DateTime CreatedAt { get; set; }
}