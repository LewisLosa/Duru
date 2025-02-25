namespace Duru.Data.Entities;

public class ReservationService : IEntity
{
    public int Id { get; set; }
    public int ReservationId { get; set; }
    public int ServiceId { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }
}