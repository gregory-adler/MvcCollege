using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcCollege.Data;
using MvcCollege.Models;

namespace MvcCollege.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _studentRepository.getAllStudentsAsync());
        }

        public IActionResult Create()
        {
            return View();
        }
        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
           [Bind("EnrollmentDate,FirstMidName,LastName")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _studentRepository.createStudent(student);
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(student);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int studentID = id ?? -1;
            var student = await _studentRepository.getStudentDetails(studentID);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        public async Task<IActionResult> Edit(int id)
        {

            int studentID = id;
            var student = await _studentRepository.getStudent(studentID);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnrollmentDate,FirstMidName,LastName")] Student student)
        {
            int studentID = id;
            var studentToUpdate = await _studentRepository.getStudent(studentID);
            if (studentToUpdate != null) { 
                try
                {
                    await _studentRepository.updateStudent(studentID, student);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(studentToUpdate);
        }
        public async Task<IActionResult> Delete(int id)
        {

            int studentID = id;
            var student = await _studentRepository.getStudent(studentID);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, bool saveChangesErorr = false)
        {
            var student = await _studentRepository.getStudent(id);
            {
                if (student == null)
                {
                    RedirectToAction("Index");
                }
                try
                {
                    await _studentRepository.deleteStudent(id);
                    return RedirectToAction("Index");
                } 
                catch
                {
                    ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
                }
                return View(student);
               
            }

        }
    }
}