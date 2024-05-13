namespace Exemplo.Domain.Config;

public class TemplateConfig
{
    public TemplateConfig()
    {
    }
    public string UrlBase { get; set; }        
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string GrantType { get; set; }
    public int MinutosExpiracaoCache { get; set; }
}


