using Dapper;
using Duru.Data.Entities;

namespace Duru.Data.Repositories
{
    public class FeedbackRepository : BaseRepository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<int> InsertAsync(Feedback entity)
        {
            using var connection = _dbContext.CreateConnection();
            var sql =
                "INSERT INTO Feedbacks (ReservationId, Rating, Comment, CreatedAt) VALUES (@ReservationId, @Rating, @Comment, @CreatedAt); SELECT last_insert_rowid();";
            return await connection.ExecuteScalarAsync<int>(sql, entity);
        }

        public override async Task<int> UpdateAsync(Feedback entity)
        {
            using var connection = _dbContext.CreateConnection();
            var sql = "UPDATE Feedbacks SET ReservationId = @ReservationId, Rating = @Rating, Comment = @Comment, CreatedAt = @CreatedAt WHERE Id = @Id";
            return await connection.ExecuteAsync(sql, entity);
        }

        public async Task<IEnumerable<Feedback>> GetFeedbacksByReservationIdAsync(int reservationId)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<Feedback>("SELECT * FROM Feedbacks WHERE ReservationId = @ReservationId",
                new { ReservationId = reservationId });
        }
    }
}
