using Duru.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Duru.Data.Repositories;

public class EmployeeRepository : BaseRepository< Employee>, IEmployeeRepository
{
    public EmployeeRepository(IDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<int> InsertAsync(Employee entity)
    {
        using var connection = _dbContext.CreateConnection();
        var sql =
            "INSERT INTO Employees (FirstName, LastName, Position, Email, Phone, HireDate, Status, CreatedAt) VALUES (@FirstName, @LastName, @Position, @Email, @Phone, @HireDate, @Status, @CreatedAt); SELECT last_insert_rowid();";
        return await connection.ExecuteScalarAsync<int>(sql, entity);
    }

    public override async Task<int> UpdateAsync(Employee entity)
    {
        using var connection = _dbContext.CreateConnection();
        var sql = "UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, Position = @Position, Email = @Email, Phone = @Phone, HireDate = @HireDate, Status = @Status, CreatedAt = @CreatedAt WHERE Id = @Id";
        return await connection.ExecuteAsync(sql, entity);
    }
}
