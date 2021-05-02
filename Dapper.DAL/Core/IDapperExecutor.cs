using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Dapper.DAL.Core
{
    public interface IDapperExecutor
    {
        Task<IEnumerable<T>> ExecuteQuery<T>(Func<SqlConnection, Task<IEnumerable<T>>> operation);
        Task<T> ExecuteQuery<T>(Func<SqlConnection, Task<T>> operation);
        Task ExecuteQuery<T>(Func<SqlConnection, Task> operation);
        Task<T> FirstOrDefaultAsync<T>(string sql, object parameters=null);
        Task<IEnumerable<T>> GetAll<T>(string sql, object parameters=null);
        Task<int> Insert<T>(string sql, object parameters=null);
    }
}