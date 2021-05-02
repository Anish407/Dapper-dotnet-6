﻿using Dapper.DAL.Models;
using System.Threading.Tasks;

namespace Dapper.DAL.Core
{
    public interface IStudentRepository
    {
        Task<string> InsertStudent(Student student);
    }
}