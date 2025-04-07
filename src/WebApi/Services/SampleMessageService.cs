using System.Runtime.CompilerServices;
using WebApi.Infrastructure.Repositories;
using WebApi.Models;

namespace WebApi.Services
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
