using Duru.Core.Models;

namespace Duru.Core.Repositories
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<IEnumerable<Payment>> GetPaymentsByReservationIdAsync(int reservationId);
        Task<IEnumerable<Payment>> GetPaymentsByStatusAsync(string status);
    }
}
