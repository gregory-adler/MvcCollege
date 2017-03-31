using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcCollege.Data;
using MvcCollege.Models;

namespace ContosoUniversity.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly IDepartmentsRepository _departmentsRepository;
        private readonly SchoolContext _context;

        public CoursesController(ICoursesRepository coursesRepository, IDepartmentsRepository departmentsRepository, SchoolContext context)
        {
            _coursesRepository = coursesRepository;
            _departmentsRepository = departmentsRepository;
            _context = context;
        }
        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var courses = _coursesRepository.getCourses();
            return View(await courses);
        }

        public IActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseID,Credits,DepartmentID,Title")] Course course)
        {
            if (ModelState.IsValid)
            {
                await _coursesRepository.addCourse(course);
                return RedirectToAction("Index");
            }
            PopulateDepartmentsDropDownList(course.DepartmentID);
            return View(course);
        }

        public async Task<IActionResult> Edit(int id)
        {

            var course = await _coursesRepository.getCourse(id);
            if (course == null)
            {
                return NotFound();
            }
            PopulateDepartmentsDropDownList(course.DepartmentID);
            return View(course);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id, [Bind("EnrollmentDate,FirstMidName,LastName")] Course course)
        {
            var courseToUpdate = await _coursesRepository.getCourse(id);

            if (courseToUpdate == null)
            {
                return NotFound();
            }

            try
            {
                await _coursesRepository.updateCourse(id, course);
            }
            catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                return RedirectToAction("Index");
            }

            PopulateDepartmentsDropDownList(courseToUpdate.DepartmentID);

            return View(courseToUpdate);
        }

        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = from d in _context.Departments
                                   orderby d.Name
                                   select d;
            ViewBag.DepartmentID = new SelectList(departmentsQuery.AsNoTracking(), "DepartmentID", "Name", selectedDepartment);
        }
    }
}