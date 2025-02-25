using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using Duru.Data;
using Microsoft.Data.Sqlite;
using SQLitePCL;
using Xunit;

namespace Duru.Data.Tests;

public class DatabaseInitializerTests : IDisposable
{
    private readonly string _dbPath;
    private readonly string _connectionString;

    public DatabaseInitializerTests()
    {
        Batteries.Init();

        _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test_duru.db");
        _connectionString = $"Data Source={_dbPath}";

        if (File.Exists(_dbPath))
            File.Delete(_dbPath);
    }

    [Fact]
    public async Task InitializeAsync_CreatesExpectedTables()
    {
        var dbContext = new SqliteDbContext(_connectionString);

        await DatabaseInitializer.InitializeAsync(dbContext);

        using (var connection = new SqliteConnection(_connectionString))
        {
            await connection.OpenAsync();

            var expectedTables = new[]
            {
                "Employees",
                "Feedbacks",
                "Guests",
                "MaintenanceRequests",
                "Payments",
                "Reservations",
                "ReservationServices",
                "Rooms",
                "RoomTypes",
                "Services"
            };

            foreach (var table in expectedTables)
            {
                var sql = "SELECT name FROM sqlite_master WHERE type='table' AND name=@TableName;";
                var result = await connection.QueryFirstOrDefaultAsync<string>(sql, new { TableName = table });
                Assert.NotNull(result);
            }
        }
    }

    public void Dispose()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();

        if (File.Exists(_dbPath))
            try
            {
                File.Delete(_dbPath);
            }
            catch (IOException ex)
            {
                Debug.WriteLine($"An error occurred when deleting database table: {ex.Message}");
            }
    }
}