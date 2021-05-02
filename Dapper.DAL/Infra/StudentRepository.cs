using Dapper.DAL.Core;
using Dapper.DAL.Models;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.DAL.Queries.StudentQuery;

namespace Dapper.DAL.Infra
{
    public class StudentRepository : IStudentRepository
    {
        public StudentRepository(IDapperExecutor dapperExecutor) => DapperExecutor = dapperExecutor;

        public IDapperExecutor DapperExecutor { get; }

        public async Task<string> InsertStudent(Student student)
        {
            return await this.DapperExecutor.ExecuteQuery<string>(async con =>
            {
                var response = await con.QueryAsync<string>(InsertStudentSql, student);
                return response.Single();
            });
        }
    }
}
