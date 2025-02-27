using Duru.Core.Models;

namespace Duru.Core.Repositories
{
    public interface IGuestRepository : IRepository<Guest>
    {
        Task<IEnumerable<Guest>> SearchGuestsByNameAsync(string searchTerm);
    }
}
