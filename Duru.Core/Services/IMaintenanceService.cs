using Duru.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duru.Core.Services
{
    public interface IMaintenanceService
    {
        Task<IEnumerable<MaintenanceRequest>> GetAllMaintenanceRequestsAsync();
        Task<MaintenanceRequest> GetMaintenanceRequestByIdAsync(int id);
        Task<int> CreateMaintenanceRequestAsync(MaintenanceRequest request);
        Task<bool> UpdateMaintenanceRequestAsync(MaintenanceRequest request);
        Task<bool> DeleteMaintenanceRequestAsync(int id);
        Task<IEnumerable<MaintenanceRequest>> GetMaintenanceRequestsByRoomIdAsync(int roomId);
    }
}
