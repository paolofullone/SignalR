using SignalR.Models;
using System.Runtime.CompilerServices;

namespace SignalR.Infrastructure.Repositories;

public interface ISampleMessageRepository
{
    Task<IEnumerable<SampleMessage>> StreamAllAsync([EnumeratorCancellation] CancellationToken cancellationToken = default);
}