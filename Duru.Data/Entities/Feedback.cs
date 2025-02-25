namespace Duru.Data.Entities;

public class Feedback : IEntity
{
    public int Id { get; set; }
    public int ReservationId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}