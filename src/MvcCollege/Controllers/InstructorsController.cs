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
        private readonly SchoolContext _context;

        public InstructorsController(IInstructorsRepository instructorsRepository, SchoolContext context)
        {
            _instructorsRepository = instructorsRepository;
            _context = context;
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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }
    }
}
