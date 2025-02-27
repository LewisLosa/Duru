using System.Data;

namespace Duru.Data.Repositories;

public interface IDbContext
{
    IDbConnection CreateConnection();
}