using API.Auth;
using API.Models;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class userController : ApiController
    {
        private int getID(HttpRequestMessage request)
        {
            string tokenString = request.Headers.Authorization.ToString();
            return authService.authorizeUser(tokenString).userid;
        }
        [HttpPost]
        [Route("api/login")]
        public HttpResponseMessage login(loginDTO login)
        {
            var tk = authService.userLogin(login.username, login.password);
            if(tk != null)
            {
                var message = new
                {
                    userrole = authService.getUserByTokenID(tk.id).userRole,
                    token = tk
                };
                return Request.CreateResponse(HttpStatusCode.OK, message);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Invalid credential"});
        }
    }
}
