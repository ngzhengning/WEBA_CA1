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
    public class CustomerAccountsController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;

        public ApplicationDbContext Database { get; }
        //Create a Constructor, so that the .NET engine can pass in the 
        //ApplicationDbContext object which represents the database session.


        public CustomerAccountsController(UserManager<ApplicationUser> userManager,
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
        [HttpGet]
        public JsonResult Get()
        {
            List<object> customerList = new List<object>();
            var customers = Database.CustomerAccounts
                .Include(x => x.UpdatedBy)
                .Include(x => x.AccountRates)
                .OrderByDescending(x => x.CustomerAccountId);
            
            foreach(var oneCustomer in customers)
            {
                customerList.Add(new
                {
                    CustomerAccountId = oneCustomer.CustomerAccountId,
                    AccountName = oneCustomer.AccountName,
                    NumOfRates = oneCustomer.AccountRates.Count,
                    Comments = oneCustomer.Comments,
                    UpdatedBy = oneCustomer.UpdatedBy.FullName,
                    UpdatedAt = oneCustomer.UpdatedAt
                });
              
            }
            return new JsonResult(customerList);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var oneCustomer = Database.CustomerAccounts
             .Where(x => x.CustomerAccountId == id).Single();

            var response = new
            {
                CustomerAccountId = oneCustomer.CustomerAccountId,
                AccountName = oneCustomer.AccountName,
                Comments = oneCustomer.Comments,
                IsVisible = oneCustomer.IsVisible,
                UpdatedById = oneCustomer.UpdatedById,
                UpdatedAt = oneCustomer.UpdatedAt
            };
            return new JsonResult(response);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]string value)
        {   
            string customMessage = "";
            var customerNewInput = JsonConvert.DeserializeObject<dynamic>(value);
            CustomerAccount newCustomer = new CustomerAccount();

            int userId = GetUserIdFromUserInfo();

            newCustomer.AccountName = customerNewInput.AccountName.Value;
            Convert.ToBoolean(customerNewInput.IsVisible.Value);
            Boolean dd = Convert.ToBoolean(customerNewInput.IsVisible.Value);
            newCustomer.IsVisible = dd;
            if (customerNewInput.Comments.Value == "")
            {
                newCustomer.Comments = null;
            }
            else
            {
                newCustomer.Comments = customerNewInput.Comments.Value;
            }
            newCustomer.CreatedById = userId;
            newCustomer.UpdatedById = userId;
            newCustomer.CreatedAt = DateTime.Now;
            newCustomer.UpdatedAt = DateTime.Now;
              
            try
            {
                Database.CustomerAccounts.Add(newCustomer);
                Database.SaveChanges();             
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message
                          .Contains("CustomerAccount_AccountName_UniqueConstraint") == true)
                {
                    customMessage = "Unable to save Customer Account record due " +
                                "to another record having the same name : " +
                                 customerNewInput.AccountName.Value;
                    object httpFailRequestResultMessage = new { message = customMessage };
                    //Return a HTTP response of Bad Request status
                    //and embed the anonymous object's content within the message-body segmet.
                    return BadRequest(httpFailRequestResultMessage);
                }
            }

            var rateNewInput = JsonConvert.DeserializeObject<dynamic>(value);
            AccountRate newAccount = new AccountRate();

            //CustomerAccount newCustomer = new CustomerAccount();
            //var ds = Database.CustomerAccounts.Select(x => x.CustomerAccountId).Last();
            //will require to save customer first.

            //foreach (var one in ds)
            //{
            //newAccount.CustomerAccountId = ds;
            //}
            newAccount.CustomerAccountId = newCustomer.CustomerAccountId;          
            decimal rate = Convert.ToDecimal(rateNewInput.ratePerHour.Value);
            newAccount.RatePerHour = rate;

            DateTime eStartDate = Convert.ToDateTime(rateNewInput.eStartDate.Value);
            newAccount.EffectiveStartDate = eStartDate;
        
            if (rateNewInput.eEndDate.Value != null)
            {
                DateTime? eEndDate = Convert.ToDateTime(rateNewInput.eEndDate.Value);
                newAccount.EffectiveEndDate = eEndDate;
            }       
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
                message = "Saved Rate and Customer record"
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
            var customerChangeInput = JsonConvert.DeserializeObject<dynamic>(value);

            var oneCustomer = Database.CustomerAccounts
                            .Where(x => x.CustomerAccountId == id).Single();

            oneCustomer.AccountName = customerChangeInput.AccountName.Value;  
            Boolean dd = Convert.ToBoolean(customerChangeInput.IsVisible.Value);
            oneCustomer.IsVisible = dd;

            oneCustomer.Comments = customerChangeInput.Comments.Value;

            oneCustomer.UpdatedById = userId;
            oneCustomer.UpdatedAt = DateTime.Now;
            
            try
            {
                Database.SaveChanges();
            }

            catch (Exception ex)
            {
                if (ex.InnerException.Message
                     .Contains("CustomerAccount_AccountName_UniqueConstraint") == true)
                {
                    customMessage = "Unable to save Customer Account record due " +
                              "to another record having the same name : " +
                             customerChangeInput.AccountName.Value;

                    object httpFailRequestResultMessage = new { message = customMessage };
                    //Return a bad http request message to the client
                    return BadRequest(httpFailRequestResultMessage);
                }
            }
            var successRequestResultMessage = new
            {
                message = "Saved Customer Account record"
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
                var foundOneCustomer = Database.CustomerAccounts
                   .Single(x => x.CustomerAccountId == id);
                Database.CustomerAccounts.Remove(foundOneCustomer);
                Database.SaveChanges();
            }
            catch (Exception)
            {
                customMessage = "Unable to delete Customer record";
                object httpFailRequestResultMessage = new { message = customMessage };
                return BadRequest(httpFailRequestResultMessage);
            }

            var successRequestResultMessage = new
            {
                message = "Deleted Customer record"
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
