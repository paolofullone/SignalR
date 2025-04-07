using System.Data;
using System.Data.Common;

namespace WebApi.Infrastructure;

public interface IDbConnectionFactory
{
    string GetConnectionString();
    DbConnection GetConnection();
}