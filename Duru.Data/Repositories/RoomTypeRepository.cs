using Dapper;
using Duru.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duru.Data.Repositories
{
    public class RoomTypeRepository : BaseRepository<RoomType>, IRoomTypeRepository
    {
        public RoomTypeRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<int> InsertAsync(RoomType entity)
        {
            using var connection = _dbContext.CreateConnection();
            var sql =
                "INSERT INTO RoomTypes (Name, Description, Capacity, Price) VALUES (@Name, @Description, @Capacity, @Price); SELECT last_insert_rowid();";
            return await connection.ExecuteScalarAsync<int>(sql, entity);
        }

        public override async Task<int> UpdateAsync(RoomType entity)
        {
            using var connection = _dbContext.CreateConnection();
            var sql = "UPDATE RoomTypes SET Name = @Name, Description = @Description, Capacity = @Capacity, Price = @Price WHERE Id = @Id";
            return await connection.ExecuteAsync(sql, entity);
        }
    }
}
