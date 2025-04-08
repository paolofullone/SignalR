
using Microsoft.AspNetCore.SignalR.Client;
using SignalR.Models;

var taskCompSource = new TaskCompletionSource();

var taskSampleMessage = StartStreamAsync<SampleMessage>(TypeConnection.SampleMessage);

Task.WhenAll(taskSampleMessage).Wait();

await taskCompSource.Task;

static HubConnection InitializeConnection(TypeConnection type)
{
    string hubUrl = $"http://localhost:5500/{type.ToString()}";
    var connection = new HubConnectionBuilder().WithUrl(hubUrl).Build();

    connection.On<string>("ReceiveMessage", Console.WriteLine);

    return connection;
}

static object[] GetConnectinoParameters(TypeConnection type)
{
    return type switch
    {
        TypeConnection.SampleMessage => new object[] { },
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
    };
}

static async Task StartStreamAsync<T>(TypeConnection type)
{
    try
    {
        Thread.Sleep(5000); // delay to start both services together so the client doesn't request before server is ready.
        Console.WriteLine($"Starting Connection: {type.ToString()}");
        var connection = InitializeConnection(type);
        await connection.StartAsync();

        object[] args = GetConnectinoParameters(type);

        await foreach (var customer in connection.StreamAsyncCore<T>(type.ToString(), args, CancellationToken.None))
        {
            Console.WriteLine($"{type.ToString()}:{System.Text.Json.JsonSerializer.Serialize(customer)}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {type.ToString()} - {ex.Message}");
    }
}

public enum TypeConnection
{
    SampleMessage
}
