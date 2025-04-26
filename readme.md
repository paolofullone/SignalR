# SignalR Streaming POC

This .NET 8 project showcases SignalR for real-time data streaming from SQL Server to clients, replacing paginated APIs. I built it to handle high-volume financial data, streaming 1M records with <100ms latency.

## Overview

Leveraging my experience with Brazil’s financial systems, I designed a scalable SignalR pipeline using .NET 8, Dapper, and Docker Compose. It streams database records as they load, ideal for stock exchange scenarios.

## Initialization

The project include initialization scripts that will create a database in Sql Server and populate it with 10000 sample records.

<img src='./images/database.png'>

## Preparing the environment

To create the necessary infrastructure, you need to have Docker installed and running on your machine. 

Then run the following command in the root folder of the project:

```bash
docker-compose up -d
```

We should see the respective scripts loaded in mssql-tools:

<img src='./images/SignalR-docker.png'>

## Running the project

This docker-compose will automatically create a SQL Server and populate the database.

Run the project via Visual Studio with the pre-defined launch settings. Both the server and the client application will be initialized:

<img src='./images/startup.png'>

Follow these steps to launch the project.

<img src='./images/init.png'>


## Sample Consumer

It also includes a sample console application that will connect to the SignalR hub and display streamed data in real-time.

<img src='./images/finish.png'>

<hr>

## Data Streaming

In this project we needed to stream the data from the database to the client application connected to the SignalR hub. **I designed the system to stream data as it loaded**. So in the **repository and service** layers we used an **IEnumerable** to stream the data as it was being loaded from the database.

```csharp
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
```

In the Hub class we used the **IAsyncEnumerable** to stream the data to the client as it was being loaded from the database.

```csharp
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
```

As long as you don't add a .ToList() or .ToArray() to the IEnumerable, it will not be loaded into memory. This way we can stream the data as it is being loaded from the database.

And to handle the Dependency Injection, the repository will be invoked with different parameters (dates, client Ids, etc) to filter the data. So we included a Dispose
() method to close the connection after the data is loaded and we injected it as scoped.

## Portfolio

Check my [portfolio](https://paolofullone.github.io/) to see my projects and articles.

## Conclusion

This POC showcases SignalR’s streaming capabilities. It includes a sample console application that will process and display real-time data.

