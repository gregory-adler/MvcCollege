using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcCollege.Models;

namespace MvcCollege.Models
{
    public interface IStudentRepository
    {
        // gets Student
        Task<Student> getStudent(int id);
        //Returns list of students
        Task<IList<Student>> getAllStudentsAsync(string sortOrder, string searchString);

        //Details view
        Task<Student> getStudentDetails(int id);

        //Creates student
        Task createStudent(Student student);

        //Edits student
        Task updateStudent(int id, Student student);

        //Deletes student
        Task deleteStudent(int id);
    }
}
