using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace SignalR.Infrastructure.DbFactories;

public class DbConnectionFactory(string connectionString) : IDbConnectionFactory
{
    public string GetConnectionString()
    {
        return connectionString;
    }

    public DbConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }
}