namespace Duru.Core.Models;

public class Feedback
{
    public int FeedbackId { get; set; }
    public int ReservationId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}