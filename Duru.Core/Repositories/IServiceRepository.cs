using Duru.Core.Models;

namespace Duru.Core.Repositories
{
    public interface IServiceRepository : IRepository<Service>
    {
        Task<IEnumerable<Service>> GetServicesByPriceRangeAsync(double minPrice, double maxPrice);
    }
}
