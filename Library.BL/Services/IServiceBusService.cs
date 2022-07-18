using Library.BL.Messages;

namespace Library.BL.Services;

public interface IServiceBusService
{
    Task PutIntoQueue(string queueName, BookAddedMessage message);
}