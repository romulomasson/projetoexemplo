
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Exemplo.Domain.Bases;
using Exemplo.Domain.Config;
using Exemplo.Domain.Interfaces;
using Exemplo.Domain.Interfaces.Bases;
using Exemplo.Domain.Interfaces.Services;
using Exemplo.Domain.Services;
using Exemplo.Repository.Contexts;

namespace Exemplo.IoC
{
    public static class Ioc
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SendGridConfig>(configuration.GetSection(nameof(SendGridConfig)));
            services.Configure<DocuSignConfig>(configuration.GetSection(nameof(DocuSignConfig)));

            services.AddDbContextPool<ExemploContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork<ExemploContext>, ExemploContext>();

            // Base
            services.AddScoped(typeof(IApplicationBase<,>), typeof(ApplicationBase<,>));
            services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>));
            services.AddScoped(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
        }
    }
}


