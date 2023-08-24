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
    [RoutePrefix("api/busprovider/account")]
    [busProviderAuth]
    public class busProviderAccountController : ApiController
    {
        private int getID(HttpRequestMessage request)
        {
            string tokenString = request.Headers.Authorization.ToString();
            return authService.authorizeUser(tokenString).userid;
        }

        [HttpPost]
        [Route("recharge")]
        public HttpResponseMessage rechargeAccount(int ammount)
        {
            try
            {
                int bp_id = getID(Request);
                var data = busProviderAccountService.rechargeAccount(bp_id, ammount);
                string message = data ? "Ammount is added" : "Ammount is not added";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
