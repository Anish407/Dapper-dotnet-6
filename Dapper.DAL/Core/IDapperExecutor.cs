using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Dapper.DAL.Core
{
    public interface IDapperExecutor
    {
        Task<IEnumerable<T>> ExecuteQuery<T>(Func<IDbConnection, Task<IEnumerable<T>>> operation);
        Task<T> ExecuteQuery<T>(Func<IDbConnection, Task<T>> operation);
        Task ExecuteQuery<T>(Func<IDbConnection, Task> operation);
        Task<T> FirstOrDefaultAsync<T>(string sql, object parameters=null);
        Task<IEnumerable<T>> GetAll<T>(string sql, object parameters=null);
        Task<int> Insert<T>(string sql, object parameters=null);
    }
}