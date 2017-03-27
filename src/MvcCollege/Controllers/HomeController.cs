using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcCollege.Models;
using MvcCollege.Models.SchoolViewModels;

namespace MvcCollege.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAggregationRepository _aggregationRepository;
        public HomeController(IAggregationRepository aggregationRepository)
        {
            _aggregationRepository = aggregationRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            ViewData["Message"] = "Updating.";

            IList<EnrollmentDateGroup> enrollmentData = new List<EnrollmentDateGroup>();
            enrollmentData = await _aggregationRepository.groupByEnrollmentDate();

            return View(enrollmentData);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
