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
    public class CoursesRepository : ICoursesRepository
    {
        public SchoolContext db;
        public CoursesRepository(SchoolContext database)
        {
            db = database;
        }
        public async Task<IList<Course>> getCourses()
        {
            var coursesQuery =db.Courses
                .Include(c => c.Department)
                .AsNoTracking();

            IList<Course> courses = new List<Course>();
            courses = await coursesQuery.ToListAsync();
            return courses;
        }

        public async Task addCourse(Course course)
        {
            db.Add(course);
            await db.SaveChangesAsync();
        }

        public async Task<Course> getCourse(int id)
        {
            Course course = await db.Courses.AsNoTracking().SingleOrDefaultAsync(m => m.CourseID == id);
            return course;
        }

        public async Task updateCourse (int id, Course course)
        {
            var toModify = db.Courses.SingleOrDefault(s => s.ID == id);
            db.Courses.Remove(toModify);
            await db.Courses.AddAsync(course);
            await db.SaveChangesAsync();
        }
    }
}
