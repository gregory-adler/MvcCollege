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
        public async Task<Student> getStudent(int id)
        {
            var student = await db.Students.SingleOrDefaultAsync(s => s.ID == id);
            return student;

        }
        public async Task<IList<Student>> getAllStudentsAsync(string sortOrder, string searchString, int? page)
        {
            var students = from s in db.Students
                           select s;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }
            int pageSize = 3;
            return await (PaginatedList<Student>.CreateAsync(students.AsNoTracking(), page ?? 1, pageSize));

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
            var toModify = db.Students.SingleOrDefault(s => s.ID == id);
            db.Students.Remove(toModify);
            await db.Students.AddAsync(student);
            await db.SaveChangesAsync();
        }

        public async Task deleteStudent(int id)
        {
            var toDelete = db.Students.SingleOrDefault(s => s.ID == id);
            if (toDelete == null)
            {
                return;
            }
            db.Students.Remove(toDelete);
            await db.SaveChangesAsync();
        }
    }
}
