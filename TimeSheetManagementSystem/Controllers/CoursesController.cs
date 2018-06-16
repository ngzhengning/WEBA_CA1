using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TimeSheetManagementSystem.Controllers
{

    public class CoursesController : Controller
    {
        // GET: /<Courses>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ExperimentDataDrivenListBox()
        {
            return View();
            //The web application will know it needs to process the
            //ExperimentDataDrivenListBox.cshtml file.
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Update()
        {
            return View();
        }
    }
}

