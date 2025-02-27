namespace Duru.Core.Models;

public class ReservationService
{
    public int ReservationServiceId { get; set; }
    public int ReservationId { get; set; }
    public int ServiceId { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }
}