using Dapper;
using Duru.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duru.Data.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<int> InsertAsync(Payment entity)
        {
            using var connection = _dbContext.CreateConnection();
            var sql =
                "INSERT INTO Payments (ReservationId, Amount, PaymentDate, Method, Status) VALUES (@ReservationId, @Amount, @PaymentDate, @Method, @Status); SELECT last_insert_rowid();";
            return await connection.ExecuteScalarAsync<int>(sql, entity);
        }

        public override async Task<int> UpdateAsync(Payment entity)
        {
            using var connection = _dbContext.CreateConnection();
            var sql = "UPDATE Payments SET ReservationId = @ReservationId, Amount = @Amount, PaymentDate = @PaymentDate, Method = @Method, Status = @Status WHERE Id = @Id";
            return await connection.ExecuteAsync(sql, entity);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByReservationIdAsync(int reservationId)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<Payment>("SELECT * FROM Payments WHERE ReservationId = @ReservationId",
                new { ReservationId = reservationId });
        }
    }
}
