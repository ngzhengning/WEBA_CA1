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
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeSheetManagementSystem.APIs
{
    [Route("api/[controller]")]
    public class AccountDetailsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;

        public ApplicationDbContext Database { get; }
        //Create a Constructor, so that the .NET engine can pass in the 
        //ApplicationDbContext object which represents the database session.


        public AccountDetailsController(UserManager<ApplicationUser> userManager,
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
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}




        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            List<object> accountList = new List<object>();
            var accounts = Database.AccountDetails
                .Include(x => x.CustomerAccount)
                .Where(x => x.CustomerAccountId == id)
               .OrderByDescending(x => x.EffectiveEndDate).ToList();

            //string accountName = accounts.FirstOrDefault().CustomerAccount.AccountName;
            var accountName = Database.CustomerAccounts
                .Where(x => x.CustomerAccountId == id)
                .Select(x => x.AccountName).Single();

           

    
            foreach (var oneDetail in accounts)
            {
                accountList.Add(new
                {
                    DayOfWeekNumber = oneDetail.DayOfWeekNumber,
                    startTime = oneDetail.StartTimeInMinutes,
                    endTime = oneDetail.EndTimeInMinutes,
                    eStartDate = oneDetail.EffectiveStartDate,
                    eEndDate = oneDetail.EffectiveEndDate,
                    isVisible = oneDetail.IsVisible,
                    accountDetailId = oneDetail.AccountDetailId

                });
            };
            return new JsonResult(new { accountList, accountName });
        }
        [HttpGet("AccountDetail/{aid}")]
        public IActionResult AccountDetail(int aid)
        {
            var oneAccount = Database.AccountDetails
                        .Where(x => x.AccountDetailId == aid).Single();

            var oneDetail = Database.CustomerAccounts
                        .Where(x => x.CustomerAccountId == oneAccount.CustomerAccountId).Single();

            var response = new
            {
                Accountname = oneDetail.AccountName,
                CustomeraccountId = oneDetail.CustomerAccountId,
                DayOfWeekNumber = oneAccount.DayOfWeekNumber,
                startTime = oneAccount.StartTimeInMinutes,
                endTime = oneAccount.EndTimeInMinutes,
                eStartDate = oneAccount.EffectiveStartDate,
                eEndDate = oneAccount.EffectiveEndDate,
                isVisible = oneAccount.IsVisible,
                accountDetailId = oneAccount.AccountDetailId
            };
            return new JsonResult(response);
        }

        [HttpGet("GetAccountDetail/{cid}")]
        public IActionResult GetAccountDetail (int cid)
        {
            List<object> accList = new List<object>();
            var acc = Database.AccountDetails
                .Include(x => x.CustomerAccount)            
                .Where(x => x.CustomerAccountId == cid).ToList();
                foreach (var oneDetail in acc)
            {            
                    accList.Add(new
                    {
                        DayOfWeekNumber = oneDetail.DayOfWeekNumber,
                        startTime = oneDetail.StartTimeInMinutes,
                        endTime = oneDetail.EndTimeInMinutes,
                        eStartDate = oneDetail.EffectiveStartDate,
                        eEndDate = oneDetail.EffectiveEndDate,
                        AccountDetailId=oneDetail.AccountDetailId
                           
                    });
                }          
            return new JsonResult(accList);    
        }

        [HttpGet("GetRateDetail/{rid}")]
        public IActionResult GetRateDetail (int rid)
        {
            List<object> dateList = new List<object>();
            var date = Database.AccountRates
                .Include(x => x.CustomerAccount)
                .Where(x => x.CustomerAccountId == rid)
                .OrderByDescending(x=>x.EffectiveEndDate).ToList();

            foreach(var oneDate in date)
                {
                dateList.Add(new {
                    
                    eStartDate = oneDate.EffectiveStartDate,
                    eEndDate = oneDate.EffectiveEndDate
                });
             }


            return new JsonResult(dateList);
        }




        // POST api/<controller>
        [HttpPost("{id}")]
        public IActionResult Post(int id, IFormCollection value)
        {
            string customMessage = "";
            int userId = GetUserIdFromUserInfo();
            AccountDetail newDetail = new AccountDetail();

            newDetail.DayOfWeekNumber = Convert.ToInt32(value["weekDayname"]);
            newDetail.CustomerAccountId = id;
            newDetail.StartTimeInMinutes = Convert.ToInt32(value["startTime"]);
            newDetail.EndTimeInMinutes = Convert.ToInt32(value["endTime"]);
            newDetail.EffectiveStartDate = Convert.ToDateTime(value["eStartDate"]);
            //newDetail.EffectiveEndDate = Convert.ToDateTime(value["eEndDate"]);
            var y = value["eEndDate"];
            if (y != "")
            {
                newDetail.EffectiveEndDate = Convert.ToDateTime(value["eEndDate"]);
            }
            else
            {
                newDetail.EffectiveEndDate = null;
            }

            int i = Convert.ToInt32(value["isVisible"]);
            if(i!=1)
            { 
                newDetail.IsVisible = false;
            }
            else
            {
                newDetail.IsVisible = true;
            }
            try
            {
                Database.AccountDetails.Add(newDetail);
                Database.SaveChanges();
            }
            catch (Exception)
            {
                customMessage = "Unable to save Account Detail data ";
                object httpFailRequestResultMessage = new { message = customMessage };
                //Return a HTTP response of Bad Request status
                //and embed the anonymous object's content within the message-body segmet.
                return BadRequest(httpFailRequestResultMessage);

            }
            var successRequestResultMessage = new
            {
                message = "Saved Account Detail record"
            };

            OkObjectResult httpOkResult = new OkObjectResult(successRequestResultMessage);
            return httpOkResult;

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, IFormCollection value)
        {


            var oneDetail = Database.AccountDetails
                      .Where(x => x.AccountDetailId == id).Single();


            string customMessage = "";
            AccountDetail newDetail = new AccountDetail();
            int userid = GetUserIdFromUserInfo();
            oneDetail.DayOfWeekNumber = Convert.ToInt32(value["weekDayname"]);
            oneDetail.StartTimeInMinutes = Convert.ToInt32(value["startTime"]);
            oneDetail.EndTimeInMinutes = Convert.ToInt32(value["endTime"]);
            oneDetail.EffectiveStartDate = Convert.ToDateTime(value["eStartDate"]);
            //oneDetail.EffectiveEndDate = Convert.ToDateTime(value["eEndDate"]);
            var y = value["eEndDate"];
            if(y!="")
            {
                oneDetail.EffectiveEndDate=  Convert.ToDateTime(value["eEndDate"]);
            }
            else
            {
                oneDetail.EffectiveEndDate = null;
            }
       

            int i = Convert.ToInt32(value["isVisible"]);
            if (i != 1)
            {
                oneDetail.IsVisible = false;
            }
            else
            {
                oneDetail.IsVisible = true;
            }
            try
            {
                Database.SaveChanges();
            }
            catch (Exception ex)
            {
                
                customMessage = "Unable to save Account Detail data " + ex;
                object httpFailRequestResultMessage = new { message = customMessage };
                //Return a HTTP response of Bad Request status
                //and embed the anonymous object's content within the message-body segmet.
                return BadRequest(httpFailRequestResultMessage);

            }
            var successRequestResultMessage = new
            {
                message = "Saved Account Detail record"
            };

            OkObjectResult httpOkResult = new OkObjectResult(successRequestResultMessage);
            return httpOkResult;

        }
    

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string customMessage = "";
            try
            {
                var foundOneRate = Database.AccountDetails
                   .Single(x => x.AccountDetailId == id);
                Database.AccountDetails.Remove(foundOneRate);
                Database.SaveChanges();
            }

            catch (Exception)
            {
                customMessage = "Unable to delete Account Detail record";
                object httpFailRequestResultMessage = new { message = customMessage };
                return BadRequest(httpFailRequestResultMessage);
            }
            var successRequestResultMessage = new
            {
                message = "Deleted Account Detail record"
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
