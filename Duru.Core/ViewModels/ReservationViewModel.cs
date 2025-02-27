using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duru.Core.ViewModels
{
    public class ReservationViewModel
    {
        public int ReservationId { get; set; }

        [Required(ErrorMessage = "Guest is required")]
        public int GuestId { get; set; }

        public string GuestName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Room is required")]
        public int RoomId { get; set; }

        public string RoomNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Check-in date is required")]
        public DateTime CheckIn { get; set; }

        [Required(ErrorMessage = "Check-out date is required")]
        public DateTime CheckOut { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int StayDuration => (CheckOut - CheckIn).Days;

        public List<ReservationServiceViewModel>? Services { get; set; }

        public double TotalAmount { get; set; }
    }
}
