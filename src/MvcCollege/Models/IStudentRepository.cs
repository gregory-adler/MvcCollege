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
        //Returns list of students
        IList<Student> getAllStudents();

        Task<IList<Student>> getAllStudentsAsync();

        //Returns student
        // Student getStudent(int? id);

        //Creates student
        //void createStudent(Student student);

        //Edits student
        //void editStudent(int? id);

        //Deletes student
        //void deleteStudent(int? id, bool? saveChangesError = false);
    }
}
