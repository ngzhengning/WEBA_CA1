using TimeSheetManagementSystem.Models;
using TimeSheetManagementSystem.Data;
using TimeSheetManagementSystem.Services;
using TimeSheetManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
namespace TimeSheetManagementSystem.APIs
{
    [Route("api/[controller]")]
    public class RolesController : Controller
    {
        
        public ApplicationDbContext Database { get; }
        public IConfigurationRoot Configuration { get; }

        //Create a Constructor, so that the .NET engine can pass in the ApplicationDbContext object
        //which represents the database session.
        public RolesController(ApplicationDbContext database)
        {
            //The code below was useful to get my project working.
            //After that, I have referred to two online site content to improve the code
            //because having three lines of such code in every Web API controller class is definitely
            //a No No.
            /*
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            options.UseSqlServer(@"Server=IT-NB147067\SQLEXPRESS;database=InternshipManagementSystemWithSecurityDB_V1;Trusted_Connection=True;MultipleActiveResultSets=True");
            Database = new ApplicationDbContext(options.Options);
            */
            Database = database;
        }
        // GET: api/Company
        [HttpGet]
        public IActionResult Get()
        {
            //The include() feature requires Microsoft Data Entity for now.
            //The visual studio might suggest you to install some packages for the include()
            //to be recognized. Don't select them.
            var roles = Database.Roles.Select(roleItem => new { roleId = roleItem.Id, roleName = roleItem.Name }  );
            return new JsonResult(roles);
        }//end of Get()

    }
}
