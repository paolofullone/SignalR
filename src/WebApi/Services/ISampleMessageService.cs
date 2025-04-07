using System.Runtime.CompilerServices;
using WebApi.Models;

namespace WebApi.Services
{
    public interface ISampleMessageService
    {
        Task<IEnumerable<SampleMessage>> StreamAllAsync([EnumeratorCancellation] CancellationToken cancellationToken = default);
    }
}
