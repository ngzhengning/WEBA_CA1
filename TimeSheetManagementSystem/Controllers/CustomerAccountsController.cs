using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeSheetManagementSystem.Controllers
{
    public class CustomerAccountsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageCustomerAccounts()
        {
            return View();
        }

        public IActionResult CreateCustomerAccount()
        {
            return View();
        }
        public IActionResult UpdateCustomerAccount()
        {
            return View();
        }

        //[Route(template: ("Mar/{accId}"))]
        public IActionResult ManageAccountRates()
        {
            return View();
        }

        //[Route(template: ("Create/{accId}"))]
        public IActionResult CreateAccountRate()
        {
            return View();
        }

        //[Route(template: ("Update/{accId}/{rateId}"))]
        public IActionResult UpdateAccountRate()
        {
            return View();
        }
        

    }
}
