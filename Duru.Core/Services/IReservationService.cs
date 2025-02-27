using Duru.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duru.Core.Services
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();
        Task<Reservation> GetReservationByIdAsync(int id);
        Task<int> CreateReservationAsync(Reservation reservation);
        Task<bool> UpdateReservationAsync(Reservation reservation);
        Task<bool> DeleteReservationAsync(int id);
        Task<IEnumerable<Reservation>> GetReservationsByGuestIdAsync(int guestId);
        Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut);
    }
}
