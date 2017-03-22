﻿using System;
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
        public async Task<Student> getStudent(int id)
        {
            var student = await db.Students.SingleOrDefaultAsync(s => s.ID == id);
            return student;

        }
        public async Task<IList<Student>> getAllStudentsAsync()
        {
            return await db.Students.ToListAsync();

        }

        public async Task createStudent(Student student)
        {
            db.Add(student);
            await db.SaveChangesAsync();
        }
        public async Task<Student> getStudentDetails(int id)
        {
            return await db.Students
                 .Include(s => s.Enrollments)
                     .ThenInclude(e => e.Course)
                 .AsNoTracking()
                 .SingleOrDefaultAsync(m => m.ID == id);
        }

        public async Task updateStudent(int id, Student student)
        {
            var toModify = db.Students.Single(s => s.ID == id);
            db.Students.Remove(toModify);
            await db.Students.AddAsync(student);
            await db.SaveChangesAsync();
        }
    }
}
