using Duru.Core.Services;
using Duru.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duru.Core.Business
{
    public class OccupancyCalculator
    {
        private readonly IRoomService _roomService;
        private readonly IReservationService _reservationService;

        public OccupancyCalculator(IRoomService roomService, IReservationService reservationService)
        {
            _roomService = roomService;
            _reservationService = reservationService;
        }

        public async Task<DashboardViewModel> GetOccupancyStatsAsync()
        {
            var rooms = await _roomService.GetAllRoomsAsync();

            var stats = new DashboardViewModel
            {
                TotalRooms = rooms.Count(),
                AvailableRooms = rooms.Count(r => r.Status == "Available"),
                OccupiedRooms = rooms.Count(r => r.Status == "Occupied"),
                MaintenanceRooms = rooms.Count(r => r.Status == "Maintenance")
            };

            // Additional stats would be calculated here

            return stats;
        }
    }
}
