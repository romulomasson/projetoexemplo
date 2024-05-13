using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Exemplo.Domain.Entities;
using Exemplo.Domain.Interfaces.Bases;
using Exemplo.Domain.Interfaces;
using Exemplo.Domain.Interfaces.Services;

namespace Exemplo.Domain.Bases
{
    public class ApplicationBase<TContext, TEntity> : IApplicationBase<TContext, TEntity>
       //where TEntity : EntityBase
       where TContext : IUnitOfWork<TContext>
    {
        protected readonly IBaseService<TContext, TEntity> _service;
        protected readonly IUnitOfWork<TContext> _unitOfWork;

        public ApplicationBase(IUnitOfWork<TContext> context, IBaseService<TContext, TEntity> service)
        {
            this._service = service;
            _unitOfWork = context;
        }


        public virtual TEntity Save(TEntity entity)
        {
            _service.Save(entity);
            _unitOfWork.Commit();

            return entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            _service.Update(entity);
            _unitOfWork.Commit();

            return entity;
        }

        public virtual void Delete(int chave)
        {
            _service.Delete(chave);
            _unitOfWork.Commit();
        }

        public virtual TEntity Get(int id)
        {
            var entidade = _service.Get(id);
            return entidade;
        }

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return _service.GetAll(predicate);
        }

        public virtual IPagedList<TEntity> GetPaginated(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true)
        {
            return _service.GetPaginated(filter, start, limit);
        }
    }

}





