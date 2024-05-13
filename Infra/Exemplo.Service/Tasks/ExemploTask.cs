using Quartz;
using Exemplo.Domain.Interfaces.Application;
using Exemplo.Domain.Interfaces.Service;
using Exemplo.Repository.Contexts;
using System.Diagnostics;
using Exemplo.Domain.Entities;

namespace Exemplo.Service.Tasks
{
    [DisallowConcurrentExecution]
    class ExemploTask : IJob, IJobTask
    {
        private readonly IExemploApplication<ExemploContext> _application;

        public ExemploTask(IExemploApplication<ExemploContext> application)
        {
            _application = application;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            string jobName = ((Quartz.Impl.JobDetailImpl)context.JobDetail)?.Name;
            var watch = new Stopwatch();
            watch.Start();
            Console.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - Inicio da Execucao");
            int qtd = await _application.ChamadaService(new Exemplo(1, "Model de Exemplo"));
            Console.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - Resultado da Execucao: {qtd} - Tempo Percorrido: {watch.ElapsedMilliseconds}");
            watch.Stop();
        }
        public bool CanExecute()
        {
            return true;
        }
    }
}









