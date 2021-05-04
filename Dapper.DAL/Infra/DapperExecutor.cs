using Dapper.DAL.Core;
using Dapper.DAL.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Dapper.DAL.Infra
{
    public class DapperExecutor : IDapperExecutor
    {
        private DbConfiguration DbConfiguration { get; }

        public DapperExecutor(IOptions<DbConfiguration> options)   => DbConfiguration = options.Value;

        async Task<SqlConnection> InitializeConnection(string connectionstring = "")
        {
            var sqlconnection = string.IsNullOrWhiteSpace(connectionstring) ? new SqlConnection(DbConfiguration.Connection) : new SqlConnection(connectionstring);
            await sqlconnection.OpenAsync();
            return sqlconnection;
        }


        /// <summary>
        /// Pass in a delegate that returns an enumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="operation"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ExecuteQuery<T>(Func<IDbConnection, Task<IEnumerable<T>>> operation)
        {
            using IDbConnection connection = await InitializeConnection();
            return await operation(connection);
        }


        public async Task<T> ExecuteQuery<T>(Func<IDbConnection, Task<T>> operation)
        {
            try
            {
                using var connection = await InitializeConnection();
                return await operation(connection);
            }
            catch (Exception)
            {

                throw;
            }
          
        }


        /// <summary>
        /// For methods that dont return anything
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="operation"></param>
        /// <returns></returns>
        public async Task ExecuteQuery<T>(Func<IDbConnection, Task> operation)
        {
            using var connection = await InitializeConnection();
            await operation(connection);
        }


        //Samples on how to use the ExecuteQuery method
        public async Task<IEnumerable<T>> GetAll<T>(string sql, object parameters = null)
            => await ExecuteQuery(async con => await con.QueryAsync<T>(sql, parameters));

        public async Task<T> FirstOrDefaultAsync<T>(string sql, object parameters = null)
            => await ExecuteQuery(async con => await con.QueryFirstOrDefaultAsync<T>(sql, parameters));

        public async Task<int> Insert<T>(string sql, object parameters = null)
            => await ExecuteQuery<int>(async con => await con.ExecuteAsync(sql, parameters));

    }

}
