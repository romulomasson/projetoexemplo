
using Exemplo.Domain.Entities;
using Exemplo.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Exemplo.Domain.Interfaces;
using Exemplo.Domain.Interfaces.Services;

namespace Exemplo.Domain.Interfaces.Service
{
    public interface IExemploService<TContext> : IBaseService<TContext, Exemplo>
        where TContext : IUnitOfWork<TContext>
    {
       
    }
}







