namespace Duru.Data.Entities;

public class Payment : IEntity
{
    public int Id { get; set; }
    public int ReservationId { get; set; }
    public double Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string Method { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}