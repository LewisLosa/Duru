using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duru.Core.ViewModels
{
    public class ReservationServiceViewModel
    {
        public int Id { get; set; }

        public int ReservationId { get; set; }

        [Required(ErrorMessage = "Service is required")]
        public int ServiceId { get; set; }

        public string ServiceName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
        public int Quantity { get; set; }

        public double UnitPrice { get; set; }

        public double TotalPrice { get; set; }
    }
}
