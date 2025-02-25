using Duru.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Duru.Data.Repositories;

public abstract class BaseRepository<T> : IRepository<T> where T : class, IEntity
{
    protected readonly IDbContext _dbContext;

    protected BaseRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        using var connection = _dbContext.CreateConnection();
        var tableName = typeof(T).Name + "s"; // we assume that table names are plural
        return await connection.QueryAsync<T>($"SELECT * FROM {tableName}");
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        using var connection = _dbContext.CreateConnection();
        var tableName = typeof(T).Name + "s";
        return await connection.QueryFirstOrDefaultAsync<T>($"SELECT * FROM {tableName} WHERE Id = @Id",
            new { Id = id });
    }

    /* Since Insert and Update operations are entity specific,
    waiting for implementation in concrete repository. */
    public abstract Task<int> InsertAsync(T entity);
    public abstract Task<int> UpdateAsync(T entity);

    public virtual async Task<int> DeleteAsync(int id)
    {
        using var connection = _dbContext.CreateConnection();
        var tableName = typeof(T).Name + "s";
        return await connection.ExecuteAsync($"DELETE FROM {tableName} WHERE Id = @Id", new { Id = id });
    }
}