using Dapper;
using Duru.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duru.Data.Repositories
{
    public class GuestRepository : BaseRepository<Guest>, IGuestRepository
    {
        public GuestRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<int> InsertAsync(Guest entity)
        {
            using var connection = _dbContext.CreateConnection();
            var sql =
                "INSERT INTO Guests (FirstName, LastName, Email, Phone, Address, CreatedAt) VALUES (@FirstName, @LastName, @Email, @Phone, @Address, @CreatedAt); SELECT last_insert_rowid();";
            return await connection.ExecuteScalarAsync<int>(sql, entity);
        }

        public override async Task<int> UpdateAsync(Guest entity)
        {
            using var connection = _dbContext.CreateConnection();
            var sql = "UPDATE Guests SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone, Address = @Address, CreatedAt = @CreatedAt WHERE Id = @Id";
            return await connection.ExecuteAsync(sql, entity);
        }
    }
}
