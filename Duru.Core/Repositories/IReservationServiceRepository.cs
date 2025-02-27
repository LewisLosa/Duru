using Duru.Core.Models;

namespace Duru.Core.Repositories;

public interface IReservationServiceRepository : IRepository<ReservationService>
{
    Task<IEnumerable<ReservationService>> GetServicesByReservationIdAsync(int reservationId);
    Task<int> AddServiceToReservationAsync(ReservationService reservationService);
    Task<int> RemoveServiceFromReservationAsync(int reservationServiceId);
}
