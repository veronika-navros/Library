using Azure.Messaging.ServiceBus;

namespace Library.BL.Messages;

public class BookAddedMessage //: ServiceBusMessage
{
    public DateTime TimeStamp { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
}