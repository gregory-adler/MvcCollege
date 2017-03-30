using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcCollege.Data;
using MvcCollege.Models;
using MvcCollege.Models.SchoolViewModels;

namespace MvcCollege.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly IInstructorsRepository _instructorsRepository;

        public InstructorsController(IInstructorsRepository instructorsRepository)
        {
            _instructorsRepository = instructorsRepository;
        }
        public async Task<IActionResult> Index(int? id, int? courseID)
        {
            var viewModel = new InstructorIndexData();
            viewModel.Instructors = await _instructorsRepository.getInstructors();
            if (id != null)
            {
                ViewData["InstructorID"] = id.Value;
                Instructor instructor = viewModel.Instructors.Where(
                    i => i.ID == id.Value).Single();
                viewModel.Courses = instructor.CourseAssignments.Select(s => s.Course);
            }

            if (courseID != null)
            {
                ViewData["CourseID"] = courseID.Value;
                viewModel.Enrollments = viewModel.Courses.Where(
                    x => x.CourseID == courseID).Single().Enrollments;
            }
            return View(viewModel);
        }
    }
}
