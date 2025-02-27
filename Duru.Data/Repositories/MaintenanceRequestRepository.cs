using Dapper;
using Duru.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duru.Data.Repositories
{
    public class MaintenanceRequestRepository : BaseRepository<MaintenanceRequest>, IMaintenanceRequestRepository
    {
        public MaintenanceRequestRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<int> InsertAsync(MaintenanceRequest entity)
        {
            using var connection = _dbContext.CreateConnection();
            var sql =
                "INSERT INTO MaintenanceRequests (RoomId, EmployeeId, Description, Status, CreatedAt, UpdatedAt) VALUES (@RoomId, @EmployeeId, @Description, @Status, @CreatedAt, @UpdatedAt); SELECT last_insert_rowid();";
            return await connection.ExecuteScalarAsync<int>(sql, entity);
        }

        public override async Task<int> UpdateAsync(MaintenanceRequest entity)
        {
            using var connection = _dbContext.CreateConnection();
            var sql = "UPDATE MaintenanceRequests SET RoomId = @RoomId, EmployeeId = @EmployeeId, Description = @Description, Status = @Status, UpdatedAt = @UpdatedAt WHERE Id = @Id";
            return await connection.ExecuteAsync(sql, entity);
        }

        public async Task<IEnumerable<MaintenanceRequest>> GetMaintenanceRequestsByRoomIdAsync(int roomId)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<MaintenanceRequest>("SELECT * FROM MaintenanceRequests WHERE RoomId = @RoomId",
                new { RoomId = roomId });
        }

        public async Task<IEnumerable<MaintenanceRequest>> GetMaintenanceRequestsByEmployeeIdAsync(int employeeId)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<MaintenanceRequest>("SELECT * FROM MaintenanceRequests WHERE EmployeeId = @EmployeeId",
                new { EmployeeId = employeeId });
        }

        public async Task<IEnumerable<MaintenanceRequest>> GetMaintenanceRequestsByStatusAsync(string status)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<MaintenanceRequest>("SELECT * FROM MaintenanceRequests WHERE Status = @Status",
                new { Status = status });
        }
    }
}
