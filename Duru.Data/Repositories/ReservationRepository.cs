using Dapper;
using Duru.Data.Entities;

namespace Duru.Data.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<int> InsertAsync(Reservation entity)
        {
            using var connection = _dbContext.CreateConnection();
            var sql =
                "INSERT INTO Reservations (GuestId, RoomId, CheckIn, CheckOut, Status, CreatedAt, UpdatedAt) VALUES (@GuestId, @RoomId, @CheckIn, @CheckOut, @Status, @CreatedAt, @UpdatedAt); SELECT last_insert_rowid();";
            return await connection.ExecuteScalarAsync<int>(sql, entity);
        }

        public override async Task<int> UpdateAsync(Reservation entity)
        {
            using var connection = _dbContext.CreateConnection();
            var sql = "UPDATE Reservations SET GuestId = @GuestId, RoomId = @RoomId, CheckIn = @CheckIn, CheckOut = @CheckOut, Status = @Status, UpdatedAt = @UpdatedAt WHERE Id = @Id";
            return await connection.ExecuteAsync(sql, entity);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByGuestIdAsync(int guestId)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<Reservation>("SELECT * FROM Reservations WHERE GuestId = @GuestId", new { GuestId = guestId });
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByRoomIdAsync(int roomId)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<Reservation>("SELECT * FROM Reservations WHERE RoomId = @RoomId", new { RoomId = roomId });
        }
    }
}
