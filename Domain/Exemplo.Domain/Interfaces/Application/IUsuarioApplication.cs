

using Exemplo.Domain.Entities;
using Exemplo.Domain.Interfaces;
using Exemplo.Domain.Interfaces.Bases;

namespace Exemplo.Domain.Interfaces.Application
{
    public interface IUsuarioApplication<TContext> : IApplicationBase<TContext, Usuario>
        where TContext : IUnitOfWork<TContext>
    {
        
    }
}

