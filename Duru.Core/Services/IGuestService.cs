using Duru.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duru.Core.Services
{

    public interface IGuestService
    {
        Task<IEnumerable<Guest>> GetAllGuestsAsync();
        Task<Guest> GetGuestByIdAsync(int id);
        Task<int> CreateGuestAsync(Guest guest);
        Task<bool> UpdateGuestAsync(Guest guest);
        Task<bool> DeleteGuestAsync(int id);
    }
}
