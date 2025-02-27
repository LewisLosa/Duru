using Duru.Data.Entities;

namespace Duru.Data.Repositories
{
    public interface IFeedbackRepository : IRepository<Feedback>
    {
        Task<IEnumerable<Feedback>> GetFeedbacksByReservationIdAsync(int reservationId);
    }
}
