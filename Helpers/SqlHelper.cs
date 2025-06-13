using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AirlineAPI;

public class SqlHelper
{

    public static List<T> ExecStoredProcedureWithResult<T>(
        DbContext context,
        string procedureName,
        params SqlParameter[] parameters
    ) where T : class, new()
    {
        var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
        var sql = $"EXEC {procedureName} {parameterNames}";
        return context.Set<T>().FromSqlRaw(sql, parameters).ToList();
    }

   public static void ExecStoredProcedure(
       DbContext context,
       string procedureName,
       params SqlParameter[] parameters
   )
   {
       var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
       var sql = $"EXEC {procedureName} {parameterNames}";
       context.Database.ExecuteSqlRaw(sql, parameters);
   }


}
