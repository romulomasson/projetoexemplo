using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Impl;
using Exemplo.IoC;
using Exemplo.Service.Tasks;
using Exemplo.Domain.Interfaces;
using Exemplo.Service.Helper;
using Exemplo.Domain.Config;
using Microsoft.Identity.Client;

namespace Exemplo.Service.Infra.IoC
{
    public static class Container
    {
        public static IServiceProvider Register(IConfiguration configuration)
        {
            var services = new ServiceCollection();

            Ioc.Initialize(services, configuration);

            services.AddScoped<ExemploTask>();
           
            services.RegisterScheduler();
            services.AddScoped<IUserHelper, UsuarioHelper>();

            return services.BuildServiceProvider();
        }

        public static IServiceCollection RegisterScheduler(this IServiceCollection services)
        {
            services.AddScoped(x =>
            {
                var sched = new StdSchedulerFactory().GetScheduler()
                    .GetAwaiter()
                    .GetResult();

                sched.JobFactory = new JobTaskFactory(x);
                return sched;
            });

            return services;
        }
    }
}










