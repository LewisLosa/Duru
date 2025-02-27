using Dapper;
using Duru.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duru.Data.Repositories
{
    public class ServiceRepository : BaseRepository<Service>, IServiceRepository
    {
        public ServiceRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<int> InsertAsync(Service entity)
        {
            using var connection = _dbContext.CreateConnection();
            var sql =
                "INSERT INTO Services (Name, Description, Price, CreatedAt) VALUES (@Name, @Description, @Price, @CreatedAt); SELECT last_insert_rowid();";
            return await connection.ExecuteScalarAsync<int>(sql, entity);
        }

        public override async Task<int> UpdateAsync(Service entity)
        {
            using var connection = _dbContext.CreateConnection();
            var sql = "UPDATE Services SET Name = @Name, Description = @Description, Price = @Price, CreatedAt = @CreatedAt WHERE Id = @Id";
            return await connection.ExecuteAsync(sql, entity);
        }
    }
}
