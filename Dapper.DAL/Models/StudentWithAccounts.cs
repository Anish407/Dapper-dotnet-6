using System;
using System.Collections.Generic;

namespace Dapper.DAL.Models
{
    public class StudentWithAccounts 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime DOB { get; set; }
        public List<StudentBackAccount> StudentBackAccounts { get; set; } = new List<StudentBackAccount>();
    }
}
