using System.Data;

namespace Exemplo.Domain.Interfaces
{
    public interface IGenericRepository<TOCntext>
    {
        List<T> ExecuteStoredProcedure<T, F>(F filters, string procedureName);
        List<T> ExecuteStoredProcedureWithParam<T>(string procedureName, IDataParameter[] parameters);
        IDataReader ExecuteReader(string commandText, IDataParameter[] parameters = null);
        int ExecuteNonQuery(string commandText, IDataParameter[] parameters, CommandType commandType);
    }
}



