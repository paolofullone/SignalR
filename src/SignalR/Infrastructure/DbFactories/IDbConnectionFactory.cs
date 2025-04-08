using System.Data;
using System.Data.Common;

namespace SignalR.Infrastructure.DbFactories;

public interface IDbConnectionFactory
{
    string GetConnectionString();
    DbConnection GetConnection();
}