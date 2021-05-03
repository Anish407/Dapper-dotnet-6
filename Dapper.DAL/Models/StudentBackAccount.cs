using System;

namespace Dapper.DAL.Models
{
    public class StudentBackAccount
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string Accounts { get; set; }
    }
}
