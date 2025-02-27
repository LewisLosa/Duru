using Duru.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duru.Core.Services
{

    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment> GetPaymentByIdAsync(int id);
        Task<int> CreatePaymentAsync(Payment payment);
        Task<bool> UpdatePaymentAsync(Payment payment);
        Task<bool> DeletePaymentAsync(int id);
        Task<IEnumerable<Payment>> GetPaymentsByReservationIdAsync(int reservationId);
    }
}
