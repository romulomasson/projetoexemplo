using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Exemplo.Domain.Entities;

namespace Exemplo.Domain.Interfaces.Services
{
    public interface ICacheService<TContext>
    {
        Task AtualizaCache<TEntity>(string prefixCache = "cache_", int minutosExpiracao = 999) where TEntity : EntityBase;
        List<TEntity> ObterLista<TEntity>(Expression<Func<TEntity, bool>> predicate = null, string prefixCache = "cache_") where TEntity : EntityBase;
        List<TEntity> ObterLista<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> predicate = null, bool update = false, bool isDto = false, string prefixCache = "cache_");
        List<TEntity> CarregaTabela<TEntity>(int minutosExpiracao = 999, string prefixCache = "cache_") where TEntity : EntityBase;
    }
}


