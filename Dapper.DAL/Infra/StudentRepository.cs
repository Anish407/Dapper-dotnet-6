using Dapper.DAL.Core;
using Dapper.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using static Dapper.DAL.Queries.StudentQuery;

namespace Dapper.DAL.Infra
{
    public class StudentRepository : IStudentRepository
    {
        public StudentRepository(IDapperExecutor dapperExecutor) => DapperExecutor = dapperExecutor;

        public IDapperExecutor DapperExecutor { get; }


        /// <summary>
        /// Insert a student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public async Task<string> InsertStudent(Student student)
            => await this.DapperExecutor.ExecuteQuery<string>(async con =>
                {
                    //execute qeury wraps the block in a try catch, so if anything goes wrong and the complete()
                    //on the transaction is not called then the transaction is rolled back
                    using var trnscn = new TransactionScope();
                    await con.ExecuteAsync(InsertStudentSql, student);
                    trnscn.Complete();
                    return student.Id.ToString();
                });

        /// <summary>
        /// Execute multiple queries 
        /// First command gets all the students and the second one gets all their bank accounts.
        /// Pass commandtype as store procedure if we need to execute an SP.. look at StudentRepositorySp.cs
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public async Task<StudentWithAccounts> GetStudentWithAccounts(Guid studentId)
              => await DapperExecutor.ExecuteQuery<StudentWithAccounts>(async con =>
              {
                  try
                  {
                      string id = studentId.ToString();
                      var multipleResponse = await con.QueryMultipleAsync(GetStudentWIthAccounts, new { StudentId = id });

                      var student = await multipleResponse.ReadFirstAsync<StudentWithAccounts>();
                      var accounts = await multipleResponse.ReadAsync<StudentBackAccount>();

                      if (accounts != null) student.StudentBackAccounts.AddRange(accounts);

                      return student;
                  }
                  catch (Exception ex)
                  {

                      throw;
                  }

              });


        public async Task<IEnumerable<Student>> GetStudentWithIDs(params string[] ids)
         => await DapperExecutor.ExecuteQuery<Student>(async con => await con.QueryAsync<Student>(GetStudentByIdsSql, new { Ids = ids }));
    }

}

