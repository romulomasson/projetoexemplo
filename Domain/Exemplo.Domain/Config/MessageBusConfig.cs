namespace Exemplo.Domain.Config;

public class MessageBusConfig
{
    public MessageBusConfig()
    {
    }

    public string ConnectionString { get; set; }
    public int SecondsToTimeout { get; set; }
    public int PreFetchCount { get; set; }
    public int ConsumeCount { get; set; }                
    public string ExemploTopic { get; set; }        

}









