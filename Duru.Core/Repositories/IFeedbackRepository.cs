using Duru.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duru.Core.Repositories
{
    public interface IFeedbackRepository : IRepository<Feedback>
    {
        Task<IEnumerable<Feedback>> GetFeedbackByRatingAsync(int rating);
        Task<IEnumerable<Feedback>> GetFeedbackByReservationIdAsync(int reservationId);
    }
}
