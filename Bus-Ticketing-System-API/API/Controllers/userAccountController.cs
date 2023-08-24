using API.Auth;
using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/user/account")]
    [userAuth]
    public class userAccountController : ApiController
    {
        private int getID(HttpRequestMessage request)
        {
            string tokenString = request.Headers.Authorization.ToString();
            return authService.authorizeUser(tokenString).userid;
        }

        [HttpGet]
        [Route("transaction")]
        public HttpResponseMessage transaction()
        {
            var userID = getID(Request);
            var data = userAccountService.GetTransactions(userID);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpPost]
        [Route("deposit/{ammount}")]
        public HttpResponseMessage rechargeAccount(int ammount)
        {
            try
            {
                int bp_id = getID(Request);
                var data = userAccountService.deposit(bp_id, ammount);
                string message = data ? "Ammount is added" : "Ammount is not added";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("withdraw/{ammount}")]
        public HttpResponseMessage withDraw(int ammount)
        {
            try
            {
                int bp_id = getID(Request);
                if(userAccountService.hasMoney(bp_id, ammount) == false)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "user doesn't have enough credit" });
                }
                var data = userAccountService.withdraw(bp_id, ammount);
                string message = data ? "Ammount is withdrawn" : "Ammount is not withdrawn";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
