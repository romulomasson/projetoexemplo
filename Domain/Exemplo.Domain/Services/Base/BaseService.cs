using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Exemplo.Domain.Entities;
using Exemplo.Domain.Helpers;
using Exemplo.Domain.Interfaces;
using Exemplo.Domain.Interfaces.Bases;
using Exemplo.Domain.Interfaces.Services;
using DocumentFormat.OpenXml.Vml.Office;

namespace Exemplo.Domain.Services
{
    public class BaseService<TContexto, TEntity> : IBaseService<TContexto, TEntity>
            where TEntity : EntityBase
            where TContexto : IUnitOfWork<TContexto>
    {
        protected readonly IRepositoryBase<TContexto, TEntity> _repository;
        protected readonly IUnitOfWork<TContexto> _unitOfWork;
        protected readonly IUserHelper _user;

        public BaseService(IRepositoryBase<TContexto, TEntity> repository, IUnitOfWork<TContexto> unitOfWork, IUserHelper user)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            this._user = user;
        }

        public virtual TEntity Save(TEntity entity)
        {
            entity.AtualizarUsuarioCadastro(_user.LoggedUser.Id);
            entity.AtualizarDataCadastro();
            entity.Validate();

            foreach (var property in entity.GetType().GetProperties())
            {
                if (property.PropertyType.IsGenericType &&
                    property.PropertyType.GetGenericTypeDefinition() == typeof(List<>) &&
                    typeof(EntityBase).IsAssignableFrom(property.PropertyType.GetGenericArguments()[0]))
                {
                    var list = (IEnumerable<EntityBase>)property.GetValue(entity);
                    if (list != null)
                    {
                        foreach (var childEntity in list)
                        {
                            childEntity.AtualizarUsuarioCadastro(_user.LoggedUser.Id);
                            childEntity.Validate();
                        }
                    }
                }

                if (typeof(EntityBase).IsAssignableFrom(property.PropertyType))
                {
                    var e = (EntityBase)property.GetValue(entity);
                    if (e != null)
                    {
                        e.AtualizarUsuarioCadastro(_user.LoggedUser.Id);
                        e.Validate();
                    }
                }
            }

            _repository.Save(entity);

            return entity;
        }

        public virtual List<TEntity> SaveRange(List<TEntity> list)
        {
            //entity.AtualizarUsuarioCadastro(_user.LoggedUser.Id);

            foreach (var entity in list)
            {
                entity.AtualizarDataCadastro();
                entity.Validate();
            }

            _repository.SaveRange(list);

            return list;
        }

        public virtual TEntity Update(TEntity entity)
        {
            entity.AtualizarUsuarioAlteracao(_user.LoggedUser.Id);
            entity.AtualizarDataAlteracao();
            entity.Validate();
            _repository.Update(entity);

            return entity;
        }

        public virtual void Delete(int chave)
        {
            TEntity entity = _repository.Get(chave);
            entity.Inativar();
            entity.AtualizarUsuarioAlteracao(_user.LoggedUser.Id);
            entity.AtualizarDataAlteracao();
            entity.Validate();
            _repository.Delete(entity);
        }

        public virtual TEntity Get(int id) => _repository.Get(id);
        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null) => _repository.GetAll(predicate);
        public virtual IPagedList<TEntity> GetPaginated(QueryFilter filter, int start, int limit, bool orderByDescending)
        {
            //Filtro partnerId
            var partnerId = _user.LoggedUser.partnerId;
            filter.AddFilter(nameof(partnerId), partnerId);

            return _repository.GetPaginated(filter, start, limit, orderByDescending);
        }
        public IQueryable<TEntity> GetQuaryable(Expression<Func<TEntity, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }
    }
}







