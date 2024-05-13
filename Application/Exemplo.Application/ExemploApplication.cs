using Exemplo.Domain.Bases;
using Exemplo.Domain.Interfaces;
using Exemplo.Domain.Entities;
using Exemplo.Domain.Interfaces.Application;
using Exemplo.Domain.Interfaces.Repository;
using Exemplo.Domain.Interfaces.Service;

namespace Exemplo.Application;

public class ExemploApplication<TContext> : ApplicationBase<TContext, Exemplo>, IExemploApplication<TContext>
    where TContext : IUnitOfWork<TContext>
{
    private readonly IExemploService<TContext> _service;
    public ExemploApplication(IUnitOfWork<TContext> context, IExemploService<TContext> service) 
        : base(context, service)
    {
        _service = service;
    }

    public Task<int> ChamadaService(Exemplo model)
    {
        return _service.ChamadaService(model);
    }
}
	







