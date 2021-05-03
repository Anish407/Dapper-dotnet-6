namespace Dapper.DAL.Queries
{
    public class StudentQuery
    {
        public const string InsertStudentSql = "Insert into dbo.student(Id,Name,Address,Phone,DOB) values (@Id,@Name,@Address,@Phone,@DOB);";
        
        public const string GetStudentWIthAccounts = "Select Id as StudentId,* from dbo.student where Id=@StudentId; Select * from dbo.StudentAccount where StudentId=@StudentId";

    }
}
