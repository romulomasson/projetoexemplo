using Exemplo.Domain.Entities;
using Exemplo.Domain.Interfaces.Bases;

namespace Exemplo.Domain.Interfaces.Application;

public interface IExemploApplication<TContext> : IApplicationBase<TContext, Exemplo>
    where TContext : IUnitOfWork<TContext>
{
    Task<int> ChamadaService(Exemplo model);
}
	






