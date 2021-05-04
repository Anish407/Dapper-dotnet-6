using Dapper.DAL.Core;
using Dapper.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Dapper.DAL.Infra
{
    public class StudentRepositorySP : IStudentRepository
    {
        public StudentRepositorySP(IDapperExecutor dapperExecutor)
          =>  DapperExecutor = dapperExecutor;

        public IDapperExecutor DapperExecutor { get; }

        /// <summary>
        ///  //pass the command type for executing SP's and the name of the SP instead of the SQL query
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public Task<StudentWithAccounts> GetStudentWithAccounts(Guid studentId)
         => this.DapperExecutor.ExecuteQuery(async con =>await con.QueryFirstOrDefaultAsync<StudentWithAccounts>
                                            ("SPName", new { id = studentId },commandType: CommandType.StoredProcedure));

        public Task<IEnumerable<Student>> GetStudentWithIDs(params string[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<string> InsertStudent(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
