using System.Data;
using Microsoft.Data.Sqlite;

namespace Duru.Data.Repositories;

public class SqliteDbContext : IDbContext
{
    private readonly string _connectionString;

    public SqliteDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new SqliteConnection(_connectionString);
    }
}