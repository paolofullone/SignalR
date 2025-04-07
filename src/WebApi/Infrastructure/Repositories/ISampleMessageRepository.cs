using System.Runtime.CompilerServices;
using WebApi.Models;

namespace WebApi.Infrastructure.Repositories;

public interface ISampleMessageRepository
{
    Task<IEnumerable<SampleMessage>> StreamAllAsync([EnumeratorCancellation] CancellationToken cancellationToken = default);
}