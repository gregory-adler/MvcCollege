using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcCollege.Models;


namespace MvcCollege.Models
{
    public interface ICoursesRepository
    {
        Task <IList<Course>> getCourses();

        Task addCourse(Course course);

        Task<Course> getCourse(int id);

        Task updateCourse(int id, Course course);
    }
}