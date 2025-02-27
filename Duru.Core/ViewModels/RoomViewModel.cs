using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duru.Core.ViewModels
{
    public class RoomViewModel
    {
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Room number is required")]
        [StringLength(10, ErrorMessage = "Room number cannot be longer than 10 characters")]
        public string RoomNumber { get; set; } = string.Empty;

        public int? RoomTypeId { get; set; }
        public string? RoomTypeName { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string? Status { get; set; }

        public int? Floor { get; set; }

        public double? Price { get; set; }

        public bool IsAvailable => Status == "Available";
    }
}
