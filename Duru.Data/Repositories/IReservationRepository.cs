using Duru.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duru.Data.Repositories
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetReservationsByGuestIdAsync(int guestId);
        Task<IEnumerable<Reservation>> GetReservationsByRoomIdAsync(int roomId);
    }
}
