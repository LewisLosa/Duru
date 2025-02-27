using Duru.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duru.Core.Repositories
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<IEnumerable<Room>> GetByFloorAsync(int floor);
        Task<IEnumerable<Room>> GetByRoomTypeAsync(int roomTypeId);
        Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut);
    }
}
