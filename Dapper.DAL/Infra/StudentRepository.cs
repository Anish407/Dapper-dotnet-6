using Dapper.DAL.Core;
using Dapper.DAL.Models;
using System;
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
            => await this.DapperExecutor.ExecuteQuery<string>(async con =>
                {
                    await con.ExecuteAsync(InsertStudentSql, student);
                    return student.Id.ToString();
                });

        public async Task<StudentWithAccounts> GetStudentWithAccounts(Guid studentId)
              => await DapperExecutor.ExecuteQuery<StudentWithAccounts>(async con =>
              {
                  try
                  {
                      string id = studentId.ToString();
                      var multipleResponse = await con.QueryMultipleAsync(GetStudentWIthAccounts, new { StudentId = id });

                      var student = await multipleResponse.ReadAsync<StudentWithAccounts>();
                      var accounts = await multipleResponse.ReadAsync<StudentBackAccount>();

                      if (accounts != null) student.StudentBackAccounts.AddRange(accounts);

                      return student.FirstOrDefault();
                  }
                  catch (Exception ex)
                  {

                      throw;
                  }
               
              });
    }

}

