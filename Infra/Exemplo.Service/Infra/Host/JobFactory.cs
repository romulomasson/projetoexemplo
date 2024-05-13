using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Simpl;
using Quartz.Spi;

namespace Exemplo.Service.Infra.Host
{
    public class JobFactory : IJobFactory
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ConcurrentDictionary<IJob, IServiceScope> _scopes = new ConcurrentDictionary<IJob, IServiceScope>();

        public JobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var scope = _serviceProvider.CreateScope();
            IJob job;

            try
            {
                job = scope.ServiceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message} Trace: {ex.StackTrace}");
                DisposeScope(scope);
                throw ex;
            }

            if (!_scopes.TryAdd(job, scope))
            {
                DisposeScope(scope);
                Console.WriteLine($"Failed to track DI scope");
                throw new Exception("Failed to track DI scope");
            }

            return job;
        }

        public void ReturnJob(IJob job)
        {
            (job as IDisposable)?.Dispose();

            if (_scopes.TryRemove(job, out var scope))
                DisposeScope(scope);
        }
        private void DisposeScope(IServiceScope scope)
        {
            if (scope is IAsyncDisposable asyncDisposableScope)
            {
                if (GetServiceLifetime(scope.GetType()) != ServiceLifetime.Singleton)
                {
                    asyncDisposableScope.DisposeAsync().AsTask().GetAwaiter().GetResult();
                }
            }
            else if (scope is IDisposable disposableScope)
            {
                if (GetServiceLifetime(scope.GetType()) != ServiceLifetime.Singleton)
                {
                    disposableScope.Dispose();
                }
            }
        }

        private ServiceLifetime GetServiceLifetime(Type serviceType)
        {
            var serviceDescriptors = _serviceProvider.GetServices<ServiceDescriptor>();

            foreach (var descriptor in serviceDescriptors)
            {
                if (descriptor.ServiceType == serviceType)
                {
                    return descriptor.Lifetime;
                }
            }

            return ServiceLifetime.Transient;
        }
    }
}









