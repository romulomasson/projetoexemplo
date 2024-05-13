using System.Data;
using System.Linq.Expressions;
using Exemplo.Domain.Entities;

namespace Exemplo.Domain.Interfaces.Bases;

public interface IRepositoryBase<TContext, TEntity>
   where TEntity : EntityBase
   where TContext : IUnitOfWork<TContext>
{
    TEntity Save(TEntity entity);
    void SaveRange(IEnumerable<TEntity> entities);
    TEntity Update(TEntity entity);
    TEntity Update(TEntity entity, string[] ignoreProps);
    void UpdateRange(IEnumerable<TEntity> entities);
    void Delete(TEntity entity);
    void Delete(int codigo);
    TEntity Get(int id);
    TEntity GetNoTracking(int id);
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> whereExp, params Expression<Func<TEntity, object>>[] includeExps);
    IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate = null);
    IPagedList<TEntity> GetPaginated(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true, params Expression<Func<TEntity, object>>[] includes);
    void BulkInsert<T>(ICollection<T> dados, string[] colunas, string table);
    List<T> ExecuteStoredProcedureWithParam<T>(string procedureName, IDataParameter[] parameters);
    List<T> ExecuteStoredProcedure<T, F>(F filters, string procedureName);
    object ExecuteProcedureScalar(string procedureName, dynamic parameter);
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}





