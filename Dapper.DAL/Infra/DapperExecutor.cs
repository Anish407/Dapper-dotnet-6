using Dapper.DAL.Core;
using Dapper.DAL.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Dapper.DAL.Infra
{
    public class DapperExecutor : IDapperExecutor
    {
        private DbConfiguration DbConfiguration { get; }

        public DapperExecutor(IOptions<DbConfiguration> options)
        {
            DbConfiguration = options.Value;
        }

        async Task<SqlConnection> InitializeConnection(string connectionstring = "")
        {
            var sqlconnection = string.IsNullOrWhiteSpace(connectionstring) ? new SqlConnection(DbConfiguration.Connection) : new SqlConnection(connectionstring);
            await sqlconnection.OpenAsync();
            return sqlconnection;
        }

        public async Task<IEnumerable<T>> ExecuteQuery<T>(Func<SqlConnection, Task<IEnumerable<T>>> operation)
        {
            using var connection = await InitializeConnection();
            return await operation(connection);
        }

        public async Task<T> ExecuteQuery<T>(Func<SqlConnection, Task<T>> operation)
        {
            using var connection = await InitializeConnection();
            return await operation(connection);
        }

        public async Task ExecuteQuery<T>(Func<SqlConnection, Task> operation)
        {
            using var connection = await InitializeConnection();
            await operation(connection);
        }

        public async Task<IEnumerable<T>> GetAll<T>(string sql, object parameters=null)
            => await ExecuteQuery(async con => await con.QueryAsync<T>(sql, parameters));

        public async Task<T> FirstOrDefaultAsync<T>(string sql, object parameters=null)
            => await ExecuteQuery(async con => await con.QueryFirstOrDefaultAsync<T>(sql, parameters));

        public async Task<int> Insert<T>(string sql, object parameters=null)
            => await ExecuteQuery<int>(async con => await con.ExecuteAsync(sql, parameters));

    }

}
