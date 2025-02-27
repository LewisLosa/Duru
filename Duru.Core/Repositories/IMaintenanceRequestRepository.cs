using Duru.Core.Models;

namespace Duru.Core.Repositories
{
    public interface IMaintenanceRequestRepository : IRepository<MaintenanceRequest>
    {
        Task<IEnumerable<MaintenanceRequest>> GetRequestsByRoomIdAsync(int roomId);
        Task<IEnumerable<MaintenanceRequest>> GetOpenRequestsAsync();
    }
}
