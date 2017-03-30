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
    public class InstructorsController : Controller
    {
        private readonly IInstructorsRepository _instructorsRepository;

        public InstructorsController(IInstructorsRepository instructorsRepository)
        {
            _instructorsRepository = instructorsRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
