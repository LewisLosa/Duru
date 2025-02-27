using Duru.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duru.Data.Repositories
{
    public interface IMaintenanceRequestRepository : IRepository<MaintenanceRequest>
    {
        Task<IEnumerable<MaintenanceRequest>> GetMaintenanceRequestsByRoomIdAsync(int roomId);
        Task<IEnumerable<MaintenanceRequest>> GetMaintenanceRequestsByEmployeeIdAsync(int employeeId);
        Task<IEnumerable<MaintenanceRequest>> GetMaintenanceRequestsByStatusAsync(string status);
    }
}
