using Duru.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duru.Core.Repositories;

namespace Duru.Core.Services.Implementations
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            var entities = await _roomRepository.GetAllAsync();
            return entities.Select(MapToModel);
        }

        public async Task<Room> GetRoomByIdAsync(int id)
        {
            var entity = await _roomRepository.GetByIdAsync(id);
            return entity != null ? MapToModel(entity) : null;
        }

        public async Task<int> CreateRoomAsync(Room room)
        {
            var entity = MapToEntity(room);
            return await _roomRepository.InsertAsync(entity);
        }

        public async Task<bool> UpdateRoomAsync(Room room)
        {
            var entity = MapToEntity(room);
            var result = await _roomRepository.UpdateAsync(entity);
            return result;
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            var result = await _roomRepository.DeleteAsync(id);
            return result;
        }

        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut)
        {
            // This would require a more sophisticated query to check against reservations
            // For now, we'll return all rooms with "Available" status
            var rooms = await GetAllRoomsAsync();
            return rooms.Where(r => r.Status == "Available");
        }

        private Room MapToModel(Room entity)
        {
            return new Room
            {
                RoomId = entity.RoomId,
                RoomNumber = entity.RoomNumber,
                RoomTypeId = entity.RoomTypeId,
                Status = entity.Status,
                Floor = entity.Floor
            };
        }

        private Room MapToEntity(Room model)
        {
            return new Room
            {
                RoomId = model.RoomId,
                RoomNumber = model.RoomNumber,
                RoomTypeId = model.RoomTypeId,
                Status = model.Status,
                Floor = model.Floor
            };
        }
    }
}
