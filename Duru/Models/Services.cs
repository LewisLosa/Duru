namespace Duru.Models
{
    public class Services
    {
        public int ServiceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double? Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
