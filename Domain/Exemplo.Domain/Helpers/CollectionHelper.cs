using System.Data;
using System.Reflection;
using Exemplo.Domain.Attributes;

namespace Exemplo.Domain.Helpers;

public class CollectionHelper
{
    private CollectionHelper()
    {
    }

    public static DataTable ConvertTo<T>(IList<T> list)
    {
        DataTable table = CreateTable<T>();
        Type entityType = typeof(T);
        var properties = entityType.GetProperties();
        foreach (T item in list)
        {
            DataRow row = table.NewRow();
            foreach (PropertyInfo prop in properties)
            {
                if (!prop.PropertyType.IsEnum && !Attribute.IsDefined(prop, typeof(DataTableColumnIgnoreAttribute)))
                {
                    var value = prop.GetValue(item);
                    row[prop.Name] = value == null ? DBNull.Value : value;
                }
            }
            table.Rows.Add(row);
        }
        return table;
    }

    public static DataTable CreateTable<T>()
    {
        Type entityType = typeof(T);
        DataTable table = new DataTable(entityType.Name);
        var properties = entityType.GetProperties();
        foreach (PropertyInfo prop in properties)
        {
            if (!prop.PropertyType.IsEnum && !Attribute.IsDefined(prop, typeof(DataTableColumnIgnoreAttribute)))
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        }
        return table;
    }
}





