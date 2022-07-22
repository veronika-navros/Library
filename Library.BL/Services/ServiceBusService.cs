using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Library.BL.Messages;

namespace Library.BL.Services;

public class ServiceBusService : IServiceBusService
{
    private readonly string _connectionString;

    public ServiceBusService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task PutIntoQueue(string queueName, BookAddedMessage message)
    {
        var client = new ServiceBusClient(_connectionString);
        var sender = client.CreateSender(queueName);

        try
        {
            var m = new ServiceBusMessage(JsonSerializer.Serialize(message));
            await sender.SendMessageAsync(m);
        }
        finally
        {
            await sender.DisposeAsync();
            await client.DisposeAsync();
        }
    }
}