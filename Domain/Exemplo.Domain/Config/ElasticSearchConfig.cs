namespace Exemplo.Domain.Config;

public class ElasticSearchConfig
{
    public ElasticSearchConfig()
    {
    }
    public string ConnectionUri { get; set; }
    public string ConnectionPool { get; set; }
    public string Usuario { get; set; }
    public string Senha { get; set; }
    public string TrackingReversaIndex { get; set; }        
}









