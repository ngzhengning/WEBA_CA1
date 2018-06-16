using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace TimeSheetManagementSystem.Controllers
{
    [Authorize("RequireAdminRole")]
    public class UserManagerController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Update()
        {
            return View();
        }
				public IActionResult Create() {
						return View();
				}
    }
}
