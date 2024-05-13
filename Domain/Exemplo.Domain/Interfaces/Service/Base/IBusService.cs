using Azure.Core;
using Azure.Messaging.ServiceBus;

namespace Exemplo.Domain.Interfaces.Services
{
    public interface IBusService
    {
        ServiceBusSender InitSender(string connectionString, string queueOrTopic);
        ServiceBusReceiver InitReceiver(string fullyQualifiedNamespace, TokenCredential credential, string queue);
        ServiceBusReceiver InitReceiver(string connectionString, string queue);
        ServiceBusReceiver InitReceiver(string connectionString, string queue, SubQueue sq, ServiceBusReceiveMode receiveMode);
        ServiceBusReceiver InitReceiver(string fullyQualifiedNamespace, TokenCredential credential, string topic, string subscription);
        ServiceBusReceiver InitReceiver(string connectionString, string topic, string subscription);
        ServiceBusReceiver InitReceiver(string connectionString, string topic, string subscription, SubQueue sq, ServiceBusReceiveMode receiveMode);
        Task Send(string entityPath, object input);
        Task SendRange<T>(string entityPath, List<T> list);       
        ValueTask DisposeAsync();
    }
}



