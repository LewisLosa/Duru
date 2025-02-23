namespace Duru.Models
{
    public class RoomTypes
    {
        public int RoomTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? Capacity { get; set; }
        public double? Price { get; set; }
    }
}
