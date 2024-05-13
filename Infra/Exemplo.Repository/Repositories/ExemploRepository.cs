using Microsoft.EntityFrameworkCore;
using Exemplo.Domain.Bases;
using Exemplo.Domain.Interfaces;
using Exemplo.Domain.Entities;
using Exemplo.Domain.Interfaces.Repository;
using Exemplo.Repository.Contexts;

namespace Exemplo.Repository.Repositories
{
    public class ExemploRepository<TContext> : RepositoryBase<TContext, Exemplo>, IExemploRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<Exemplo> _dbSet => ((ExemploContext)UnitOfWork).Set<Exemplo>();
        public ExemploRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	









