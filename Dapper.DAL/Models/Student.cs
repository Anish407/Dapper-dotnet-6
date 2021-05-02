using System;

namespace Dapper.DAL.Models
{
    public class Student
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime DOB { get; set; }

    }
}
