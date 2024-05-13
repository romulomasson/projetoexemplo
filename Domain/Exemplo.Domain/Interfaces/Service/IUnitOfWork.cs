namespace Exemplo.Domain.Interfaces
{
    public interface IUnitOfWork<TContext>
    {
        int Commit();
    }
}



