namespace Dapper.DAL.Queries
{
    public class StudentQuery
    {
        public const string InsertStudentSql = "Insert into dbo.student(Id,Name,Address,Phone,DOB) values (@Id,@Name,@Address,@Phone,@DOB);";
    }
}
