using Dapper;
using SignalR.Infrastructure.DbFactories;
using SignalR.Models;
using System.Data;
using System.Runtime.CompilerServices;

namespace SignalR.Infrastructure.Repositories;

public class SampleMessageRepository(IDbConnectionFactory connectionFactory) : ISampleMessageRepository
{
    private readonly IDbConnection _connection = connectionFactory.GetConnection();
    public async Task<IEnumerable<SampleMessage>> StreamAllAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        const string sql = "SELECT Id, MESSAGE_ID AS MessageId, MESSAGE_DATE AS MessageDate FROM dbo.SAMPLE_MESSAGES ORDER BY Id";

        var command = new CommandDefinition(
            commandText: sql, 
            flags: CommandFlags.None, 
            cancellationToken: cancellationToken
        );

        return await _connection.QueryAsync<SampleMessage>(command).ConfigureAwait(false);
    }

    public void Dispose()
        => _connection.Dispose();
}