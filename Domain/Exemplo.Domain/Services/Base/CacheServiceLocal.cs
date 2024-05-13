using Microsoft.Extensions.Options;
using Exemplo.Domain.Config;
using Exemplo.Domain.Entities;
using Exemplo.Domain.Interfaces.Service.Base;
using Exemplo.Domain.Interfaces.Repository;
using Exemplo.Domain.Interfaces;
using Exemplo.Domain.Interfaces.Services;

namespace Exemplo.Domain.Services.Base
{
    public class CacheServiceLocal<TContext> : ICacheServiceLocal<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        private readonly TemplateConfig _config;
        private readonly string _prefix = "cache_";
        private readonly ICacheService<TContext> _service;

        public CacheServiceLocal(IOptions<TemplateConfig> config,
            ICacheService<TContext> service)
        {
            _config = config.Value;
            _service = service;
        }

        public void InicializaCache()
        {
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - Iniciando Carregamento do Cache");
            _service.CarregaTabela<Exemplo>(_config.MinutosExpiracaoCache, _prefix);
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - Fim Carregamento do Cache");
        }
    }
}






