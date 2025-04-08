using Microsoft.AspNetCore.SignalR;
using SignalR.Models;
using SignalR.Services;
using System.Runtime.CompilerServices;

namespace SignalR.Hubs;

public class SampleMessageHub(ISampleMessageService messageService) : Hub
{
    // here we return IASyncEnumerable<SampleMessage> to the client, in the service/repository layer we worked with IEnumerable<SampleMessage>, as long as we don't do a .ToList() or .ToArray() we can return the same type to the client and we will stream data as we receive from the database, not having to wait for the entire collection to be loaded in memory
    public async IAsyncEnumerable<SampleMessage> SampleMessage([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var messages = await messageService.StreamAllAsync(cancellationToken);

        foreach (var message in messages)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                yield return message;
            }
            else
            {
                break;
            }
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        // Log the disconnection or perform cleanup
        Console.WriteLine($"Client disconnected: {Context.ConnectionId}");

        // Optionally, you can notify other clients about the disconnection
        await Clients.All.SendAsync("ClientDisconnected", Context.ConnectionId);

        // Call the base implementation
        await base.OnDisconnectedAsync(exception);
    }
}