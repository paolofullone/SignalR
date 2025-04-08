using SignalR.Models;
using System.Runtime.CompilerServices;

namespace SignalR.Services
{
    public interface ISampleMessageService
    {
        Task<IEnumerable<SampleMessage>> StreamAllAsync([EnumeratorCancellation] CancellationToken cancellationToken = default);
    }
}
