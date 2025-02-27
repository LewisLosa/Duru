using Duru.Core.Models;

namespace Duru.Core.Repositories
{
    public interface IRoomTypeRepository : IRepository<RoomType>
    {
        Task<IEnumerable<RoomType>> GetActiveRoomTypesAsync();
    }
}
