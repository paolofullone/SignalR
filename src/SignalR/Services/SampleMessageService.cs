using SignalR.Infrastructure.Repositories;
using SignalR.Models;
using System.Runtime.CompilerServices;

namespace SignalR.Services
{
    public class SampleMessageService(ISampleMessageRepository repository) : ISampleMessageService
    {
        public Task<IEnumerable<SampleMessage>> StreamAllAsync(
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            return repository.StreamAllAsync(cancellationToken);
        }
    }
}
