using Duru.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Duru.Data.Repositories;

public class RoomRepository : BaseRepository<Room>, IRoomRepository
{
    public RoomRepository(IDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<int> InsertAsync(Room entity)
    {
        using var connection = _dbContext.CreateConnection();
        var sql =
            "INSERT INTO Rooms (RoomNumber, Status, Floor) VALUES (@RoomNumber, @Status, @Floor); SELECT last_insert_rowid();";
        return await connection.ExecuteScalarAsync<int>(sql, entity);
    }

    public override async Task<int> UpdateAsync(Room entity)
    {
        using var connection = _dbContext.CreateConnection();
        var sql = "UPDATE Rooms SET RoomNumber = @RoomNumber, Status = @Status, Floor = @Floor WHERE Id = @Id";
        return await connection.ExecuteAsync(sql, entity);
    }
}