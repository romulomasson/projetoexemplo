using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Exemplo.Domain.Entities;
using Exemplo.Domain.Entities;

namespace Exemplo.Domain.Interfaces.Service.Base
{
    public interface ICacheServiceLocal<TContext>
    {
        void InicializaCache();        
    }
}







