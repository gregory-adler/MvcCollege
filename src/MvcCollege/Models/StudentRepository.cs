using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcCollege.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MvcCollege.Models
{
    public class StudentRepository : IStudentRepository
    {
        public SchoolContext db;
        public StudentRepository(SchoolContext database)
        {
            db = database;
        }
        public IList<Student> getAllStudents()
        {
            return db.Students.ToList();

        }
        public async Task<IList<Student>> getAllStudents()
        {
            return db.Students.ToListAsync();

        }
    }
}
