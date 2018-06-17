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
    public class AccountRatesController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;

        public ApplicationDbContext Database { get; }
        //Create a Constructor, so that the .NET engine can pass in the 
        //ApplicationDbContext object which represents the database session.


        public AccountRatesController(UserManager<ApplicationUser> userManager,
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
        // GET: api/<controller>
        //[HttpGet]
        //public JsonResult Get()
        //{
        //    List<object> rateList = new List<object>();

        //    var rates = Database.AccountRates
        //                 .
        //}


        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            List<object> rateList = new List<object>();
            var rates = Database.AccountRates
                .Include(x => x.CustomerAccount)
                .Where(x => x.CustomerAccountId == id)
               .OrderByDescending(x => x.EffectiveEndDate).ToList();
            string accountName = rates.FirstOrDefault().CustomerAccount.AccountName;
            foreach (var oneRate in rates)
            {
                rateList.Add(new
                {
                    //CustomerAccountId = oneRate.CustomerAccountId,
                    AccountRateId = oneRate.AccountRateId,
                    //AccountName = oneRate.CustomerAccount.AccountName,
                    RatePerHour = oneRate.RatePerHour,
                    eStartDate = oneRate.EffectiveStartDate,

                    eEndDate = oneRate.EffectiveEndDate

                });

            };


            return new JsonResult(new { rateList, accountName });

        }
        [HttpGet("RateDetail/{rid}")]
        public IActionResult RateDetail(int rid)
        {
            var oneRate = Database.AccountRates
                .Where(x => x.AccountRateId == rid).Single();

            var oneCustomer = Database.CustomerAccounts
                        .Where(x => x.CustomerAccountId == oneRate.CustomerAccountId).Single();

            var response = new
            {
                // AccountRateId = oneRate.AccountRateId,
                AccountName = oneCustomer.AccountName,
                CustomerAccountId= oneCustomer.CustomerAccountId,
                RatePerHour = oneRate.RatePerHour,
                eStartDate = oneRate.EffectiveStartDate,

                eEndDate = oneRate.EffectiveEndDate
            };
             return new JsonResult(response);
        }


        [HttpGet("GetRateDetail/{cid}")]
        public IActionResult GetRateDetail(int cid)
        {
            List<object> rateList = new List<object>();
            var rates = Database.AccountRates
                .Include(x => x.CustomerAccount)
                .Where(x => x.CustomerAccountId == cid)
                .OrderByDescending(x => x.EffectiveEndDate)
                .ToList();
            foreach (var oneRate in rates)
            {
                rateList.Add(new
                {
                    //CustomerAccountId = oneRate.CustomerAccountId,
                    AccountRateId = oneRate.AccountRateId,
                    //AccountName = oneRate.CustomerAccount.AccountName,
                    RatePerHour = oneRate.RatePerHour,
                    eStartDate = oneRate.EffectiveStartDate,

                    eEndDate = oneRate.EffectiveEndDate

                });

            };
            return new JsonResult(rateList);
        }




        // POST api/<controller>
        [HttpPost("{id}")]
        public IActionResult Post(int id,[FromBody]string value)
        {
            //GG SECTION
            string customMessage = "";
            var rateNewInput = JsonConvert.DeserializeObject<dynamic>(value);

            //retrieve ID 
            var oneSession = Database.CustomerAccounts   
               .Where(x => x.CustomerAccountId == id).Single();


            //List<object> rateList = new List<object>();   
            //var test = Database.AccountRates
            //        .Where(x=>x.CustomerAccountId==oneSession.CustomerAccountId).ToList();

           
            //var oneDb = Database.AccountRates
            //            .Include(x => x.RatePerHour);

            //var ds = Database.CustomerAccounts
            //    .Select(x => x.CustomerAccountId).ToList();
            //foreach (var one in ds)
            //{
            //}

            AccountRate newAccount = new AccountRate();
            int userid = GetUserIdFromUserInfo();
            newAccount.CustomerAccountId = oneSession.CustomerAccountId;
            oneSession.UpdatedAt = DateTime.Now;
            oneSession.UpdatedById = userid;
            //newAccount.CustomerAccountId = newCustomer.CustomerAccountId;
            decimal rate = Convert.ToDecimal(rateNewInput.ratePerHour.Value);
            
            newAccount.RatePerHour = rate;


            DateTime eStartDate = Convert.ToDateTime(rateNewInput.eStartDate.Value);
            newAccount.EffectiveStartDate = eStartDate;

            if (rateNewInput.eEndDate.Value != null)
            {
                //newAccount.EffectiveEndDate = null;
                DateTime? eEndDate = Convert.ToDateTime(rateNewInput.eEndDate.Value);
                newAccount.EffectiveEndDate = eEndDate;
            } 
            //else
            //{
            //    DateTime eEndDate = Convert.ToDateTime(rateNewInput.eEndDate.Value);
            //    newAccount.EffectiveEndDate = rateNewInput.eEndDate.Value;
            //    DateTime? eEndDate = Convert.ToDateTime(rateNewInput.eEndDate.Value);
            //    newAccount.EffectiveEndDate = eEndDate;
            //}

            try
            {
                Database.AccountRates.Add(newAccount);
                Database.SaveChanges();
            }

            catch (Exception)
            {
                customMessage = "Unable to save Rates data ";
                object httpFailRequestResultMessage = new { message = customMessage };
                //Return a HTTP response of Bad Request status
                //and embed the anonymous object's content within the message-body segmet.
                return BadRequest(httpFailRequestResultMessage);

            }
            var successRequestResultMessage = new
            {
                message = "Saved Rate record"
            };

            OkObjectResult httpOkResult = new OkObjectResult(successRequestResultMessage);
            return httpOkResult;
        }//end of post



        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]string value)
        {
            string customMessage = "";
            var rateChangeInput = JsonConvert.DeserializeObject<dynamic>(value);

            var oneRate = Database.AccountRates
                        .Where(x => x.AccountRateId == id).Single();

            var oneCustomer = Database.CustomerAccounts
                         .Where(x => x.CustomerAccountId == oneRate.CustomerAccountId).Single();

            int userId = GetUserIdFromUserInfo();
            oneCustomer.UpdatedById = userId;
            oneCustomer.UpdatedAt = DateTime.Now;


            decimal rate = Convert.ToDecimal(rateChangeInput.ratePerHour.Value);
            oneRate.RatePerHour = rate;
         

            DateTime eStartDate = Convert.ToDateTime(rateChangeInput.eStartDate.Value);
            oneRate.EffectiveStartDate = eStartDate;

            if (rateChangeInput.eEndDate.Value != null)
            {
                
                DateTime? eEndDate = Convert.ToDateTime(rateChangeInput.eEndDate.Value);
                oneRate.EffectiveEndDate = eEndDate;
            }
            try
            {
                Database.SaveChanges();
            }
            catch(Exception)
            {
                customMessage = "Unable to save Rate Account record";
                object httpFailRequestResultMessage = new { message = customMessage };
                //Return a bad http request message to the client
                return BadRequest(httpFailRequestResultMessage);
            }
            var successRequestResultMessage = new
            {
                message = "Saved Rate Account record"
            };
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
                var foundOneRate = Database.AccountRates
                   .Single(x => x.AccountRateId == id);
                Database.AccountRates.Remove(foundOneRate);
                Database.SaveChanges();
            }

            catch (Exception ex)
            {
                customMessage = "Unable to delete Rate record";
                object httpFailRequestResultMessage = new { message = customMessage };
                return BadRequest(httpFailRequestResultMessage);
            }
            var successRequestResultMessage = new
            {
                message = "Deleted rate record"
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
