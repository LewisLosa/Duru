using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Duru.Data.Entities;
using Duru.Data.Repositories;

namespace Duru.Data;

public static class DatabaseInitializer
{
    public static async Task InitializeAsync(IDbContext dbContext)
    {
        using (var connection = dbContext.CreateConnection())
        {
            try
            {
                // Use OpenAsync method from DbConnection
                if (connection is DbConnection dbConnection)
                    await dbConnection.OpenAsync();
                else
                    connection.Open();

                var assembly = Assembly.GetAssembly(typeof(IEntity));
                if (assembly == null)
                {
                    throw new InvalidOperationException("Unable to get the assembly containing IEntity.");
                }

                var entityTypes = assembly
                    .GetTypes()
                    .Where(t => typeof(IEntity).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                    .ToList();


                foreach (var type in entityTypes)
                {
                    var createTableSql = GenerateCreateTableScript(type);
                    await connection.ExecuteAsync(createTableSql);
                    Console.WriteLine($"Tablo '{type.Name + "s"}' oluşturuldu.");
                }

                Console.WriteLine("Veritabanı ve tablolar başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Veritabanı oluşturulurken hata oluştu: {ex.Message}");
            }
        }
    }

    private static string GenerateCreateTableScript(Type type)
    {
        var tableName = type.Name + "s";
        var sb = new StringBuilder();
        sb.AppendLine($"CREATE TABLE IF NOT EXISTS {tableName} (");

        var properties = type.GetProperties();
        var columnDefs = properties.Select(prop => GetColumnDefinition(prop)).ToArray();

        sb.AppendLine(string.Join(",\n", columnDefs));
        sb.Append(");");

        return sb.ToString();
    }

    private static string GetColumnDefinition(PropertyInfo prop)
    {
        var columnName = prop.Name;
        var columnType = GetSqliteType(prop.PropertyType);

        if (columnName.Equals("Id", StringComparison.OrdinalIgnoreCase) &&
            (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?)))
            return $"{columnName} INTEGER PRIMARY KEY AUTOINCREMENT";
        else
            return $"{columnName} {columnType}";
    }

    private static string GetSqliteType(Type type)
    {
        var underlyingType = Nullable.GetUnderlyingType(type) ?? type;

        if (underlyingType == typeof(int) || underlyingType == typeof(long) || underlyingType == typeof(bool))
            return "INTEGER";
        if (underlyingType == typeof(float) || underlyingType == typeof(double) || underlyingType == typeof(decimal))
            return "REAL";
        if (underlyingType == typeof(DateTime))
            return "TEXT";
        return "TEXT";
    }
}