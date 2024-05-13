
using  Exemplo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Exemplo.Domain.Interfaces.Repository;
using Exemplo.Domain.Interfaces;
using Exemplo.Domain.Interfaces.Bases;

namespace  Exemplo.Domain.Interfaces.Repository
{
    public interface IExemploRepository<TContext> : IRepositoryBase<TContext, Exemplo> 
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
	







