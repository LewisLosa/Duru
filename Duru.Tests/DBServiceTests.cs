using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using Duru.Data;
using Duru.Data.Entities;
using Duru.Data.Repositories;
using Microsoft.Data.Sqlite;
using SQLitePCL;
using Xunit;

namespace Duru.Data.Tests;

public class DatabaseInitializerTests : IDisposable
{
    private readonly string _dbPath;
    private readonly string _connectionString;
    private readonly SqliteDbContext _dbContext;
    private readonly RoomRepository _roomRepository;
    private readonly EmployeeRepository _employeeRepository;


    public DatabaseInitializerTests()
    {
        Batteries.Init();

        _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test_duru.db");
        _connectionString = $"Data Source={_dbPath};Mode=ReadWriteCreate;Cache=Shared";
        _dbContext = new SqliteDbContext(_connectionString);
        _roomRepository = new RoomRepository(_dbContext);
        _employeeRepository = new EmployeeRepository(_dbContext);

        if (File.Exists(_dbPath))
        {
            try
            {
                File.Delete(_dbPath);
            }
            catch (IOException ex)
            {
                Debug.WriteLine($"[WARN] Could not delete database file: {ex.Message}");
            }
        }
    }

    [Fact]
    public async Task InitializeAndVerifyDatabase()
    {
        await DatabaseInitializer.InitializeAsync(_dbContext);

        using (var connection = new SqliteConnection(_connectionString))
        {
            await connection.OpenAsync();

            var sql = "SELECT name FROM sqlite_master WHERE type='table' AND name='Rooms';";
            var result = await connection.QueryFirstOrDefaultAsync<string>(sql);

            Assert.NotNull(result); 
        }
        await InsertTestData();
        await VerifyTableData();
    }

    private async Task InsertTestData()
    {
        var room1 = new Room { RoomNumber = "101", Status = "Available", Floor = 1 };
        var room2 = new Room { RoomNumber = "102", Status = "Occupied", Floor = 1 };
        var room3 = new Room { RoomNumber = "103", Status = "Available", Floor = 2 };
        var employee1 = new Employee
        {
            FirstName = "Eyup",
            LastName = "Sengoz",
            Email = "eyupsengoz@losa.dev",
            CreatedAt = DateTime.Now,
            HireDate = DateTime.Now,
            Phone = "1234567890",
            Position = "Fullstack Developer",
            Status = "Active"
        };
        var employee2 = new Employee
        {
            FirstName = "Mehmet Nedim",
            LastName = "Gül",
            Email = "mnedimgul@gmail.com",
            CreatedAt = DateTime.Now,
            HireDate = DateTime.Now,
            Phone = "1234567804",
            Position = "Frontend Developer",
            Status = "Active"
        };
        var employee3 = new Employee
        {
            FirstName = "Zeynep",
            LastName = "Hazar",
            Email = "zeynephazar@gmail.com",
            CreatedAt = DateTime.Now,
            HireDate = DateTime.Now,
            Phone = "1234567890",
            Position = "Graphic Designer",
            Status = "Active"
        };

        await _roomRepository.InsertAsync(room1);
        await _roomRepository.InsertAsync(room2);
        await _roomRepository.InsertAsync(room3);
        await _employeeRepository.InsertAsync(employee1);
        await _employeeRepository.InsertAsync(employee2);
        await _employeeRepository.InsertAsync(employee3);
    }

    private async Task VerifyTableData()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            await connection.OpenAsync();

            var selectRooms = await connection.QueryAsync<Room>("SELECT * FROM Rooms");
            var selectEmployees = await connection.QueryAsync<Employee>("SELECT * FROM Employees");

            Assert.NotEmpty(selectRooms);
            foreach (var room in selectRooms)
            {
                Debug.WriteLine($"Room: {room.RoomNumber}, Status: {room.Status}, Floor: {room.Floor}");
            }

            Assert.NotEmpty(selectEmployees);
            foreach (var employee in selectEmployees)
            {
                Debug.WriteLine($"FirstName: {employee.FirstName}, LastName: {employee.LastName}, Email: {employee.Email}, Position: {employee.Position}, Status: {employee.Status}");
            }
        }
    }

    public void Dispose()
    {
        try
        {
            SqliteConnection.ClearAllPools();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (File.Exists(_dbPath))
            {
                File.Delete(_dbPath);
            }
        }
        catch (IOException ex)
        {
            Debug.WriteLine($"An error occurred when deleting database file: {ex.Message}");
        }
    }

}
