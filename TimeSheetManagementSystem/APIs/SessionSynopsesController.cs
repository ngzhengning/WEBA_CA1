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
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeSheetManagementSystem.APIs
{
    [Route("api/[controller]")]
    public class SessionSynopsesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;

        public ApplicationDbContext Database { get; }
        //Create a Constructor, so that the .NET engine can pass in the 
        //ApplicationDbContext object which represents the database session.


            public SessionSynopsesController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory, ApplicationDbContext database)
        {
            Database = database; //Initialize the Database property

            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }




        //public SessionSynopsesController()
        //{
        //    Database = new ApplicationDbContext();

        //}


        [HttpGet]
        public JsonResult Get()
        {
            List<object> sessionList = new List<object>();
            var sessions = Database.SessionSynopses
                  .Include(input => input.CreatedBy)
                .Include(input => input.UpdatedBy)
                .OrderByDescending(x => x.SessionSynopsisId);
            foreach (var oneSession in sessions)
            {
                sessionList.Add(new
                {
                    SessionSynopsisId = oneSession.SessionSynopsisId,
                    SessionSynopsisName = oneSession.SessionSynopsisName,
                    IsVisible = oneSession.IsVisible,
                    CreatedById = oneSession.CreatedById,
                    UpdatedById = oneSession.UpdatedById ,
                    UpdatedBy = oneSession.UpdatedBy.FullName,
                    CreatedBy = oneSession.CreatedBy.FullName

           
                    
                });
            }//end of foreach
            return new JsonResult(sessionList);
            
        }//end of Get()
     



       // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var oneSession = Database.SessionSynopses
                .Where(x=>x.SessionSynopsisId == id).Single();

            var response = new
            {
                SessionSynopsisId = oneSession.SessionSynopsisId,
                SessionSynopsisName = oneSession.SessionSynopsisName,
                IsVisible = oneSession.IsVisible,
                CreatedById = oneSession.CreatedById,
                UpdatedById = oneSession.UpdatedById

            };
            return new JsonResult(response);

        }



        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]string value)
        {

            string customMessage = "";
            var sessionNewInput = JsonConvert.DeserializeObject<dynamic>(value);
            SessionSynopsis newSession = new SessionSynopsis();


            int userId = GetUserIdFromUserInfo();
            newSession.SessionSynopsisName = sessionNewInput.SessionSynopsisName.Value;
            Convert.ToBoolean(sessionNewInput.IsVisible.Value);
            Boolean dd = Convert.ToBoolean(sessionNewInput.IsVisible.Value);
            newSession.IsVisible = dd;
            newSession.CreatedById = userId;
            newSession.UpdatedById = userId;


            try
            {

                Database.SessionSynopses.Add(newSession);
                Database.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message
                          .Contains("SessionSynopsis_SessionSynopsisName_UniqueConstraint") == true)
                {
                    customMessage = "Unable to save session record due " +
                                "to another record having the same name : " +
                                 sessionNewInput.SessionSynopsisName.Value;
                    object httpFailRequestResultMessage = new { message = customMessage };
                    //Return a HTTP response of Bad Request status
                    //and embed the anonymous object's content within the message-body segmet.
                    return BadRequest(httpFailRequestResultMessage);
                }
            }
                var successRequestResultMessage = new
                {
                    message = "Saved session record"
                };

                //Create a OkObjectResult class instance, httpOkResult.
                //When creating the OkObjectResult class instance, provide 
                //the anonymous object, successRequestResultMessage into it.
                OkObjectResult httpOkResult = new OkObjectResult(successRequestResultMessage);
                //Send the OkObjectResult class object back to the client.
                return httpOkResult;
            
        }




        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]string value)
        {
            string customMessage = "";
            int userId = GetUserIdFromUserInfo();
            var sessionChangeInput = JsonConvert.DeserializeObject<dynamic>(value);

            var oneSession = Database.SessionSynopses
                .Where(x => x.SessionSynopsisId == id).Single();

            oneSession.SessionSynopsisName = sessionChangeInput.SessionSynopsisName.Value;

            //Convert.ToBoolean(sessionChangeInput.IsVisibile.Value);
            Boolean dd = Convert.ToBoolean(sessionChangeInput.IsVisible.Value);
            oneSession.IsVisible = dd;
            // oneSession.IsVisible = sessionChangeInput.IsVisible.Value;
            oneSession.UpdatedById = userId;
           try
            {
                Database.SaveChanges();
            }
            catch(Exception ex)
            {
                if(ex.InnerException.Message
                     .Contains("SessionSynopsis_SessionSynopsisName_UniqueConstraint") == true)
                {
                    customMessage = "Unable to save session record due " +
                              "to another record having the same name : " +
                              sessionChangeInput.SessionSynopsisName.Value;

                    object httpFailRequestResultMessage = new { message = customMessage };
                    //Return a bad http request message to the client
                    return BadRequest(httpFailRequestResultMessage);
                }
            }
            var successRequestResultMessage = new
            {
                message = "Saved session record"
            };

            //Create a OkObjectResult class instance, httpOkResult.
            //When creating the object, provide the previous message object into it.
            OkObjectResult httpOkResult =
                                    new OkObjectResult(successRequestResultMessage);
            //Send the OkObjectResult class object back to the client.
            return httpOkResult;
        }





        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string customMessage = "";
            try
            {
                var foundOneSession = Database.SessionSynopses
                     .Single(x => x.SessionSynopsisId == id);
                Database.SessionSynopses.Remove(foundOneSession);
                Database.SaveChanges();
            }
            catch(Exception ex)
            {
                customMessage = "Unable to delete session record";
                object httpFailRequestResultMessage = new { message = customMessage };
                return BadRequest(httpFailRequestResultMessage);
            }

            var successRequestResultMessage = new
            {
                message = "Deleted session record"
            };
            OkObjectResult httpOkResult =
                       new OkObjectResult(successRequestResultMessage);
            //Send the OkObjectResult class object back to the client.
            return httpOkResult;
        }

        public int GetUserIdFromUserInfo()
        {
            string userLoginId = _userManager.GetUserName(User);
            int userInfoId = Database.UserInfo.Single(input => input.LoginUserName == userLoginId).UserInfoId;
            return userInfoId;
        }
    }
}
