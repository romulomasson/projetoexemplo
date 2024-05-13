using AutoMapper;
using Exemplo.Domain.Entities;
using Exemplo.Domain.Interfaces;
using Exemplo.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exemplo.Domain.Interfaces;
using Exemplo.Domain.Entities;

namespace Exemplo.Api.AutoMapper
{
    public class PagedListConverter<TEntity, TViewModel> //: ITypeConverter<PagedList<TEntity>, PagedList<TViewModel>>
    {        
        public PagedList<TViewModel> Convert(IPagedList<TEntity> source, IMapper _mapper)
        {
            var vm = source.Data.Select(item => _mapper.Map<TViewModel>(item)).ToList();
            return new PagedList<TViewModel>(vm, source.Total);
        }
    }
}









