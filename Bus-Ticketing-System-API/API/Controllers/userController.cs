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
    [RoutePrefix("api/user")]
    public class userController : ApiController
    {
        private int getID(HttpRequestMessage request)
        {
            string tokenString = request.Headers.Authorization.ToString();
            return authService.authorizeUser(tokenString).userid;
        }
        [HttpPost]
        [Route("login")]
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
        [HttpPost]
        [Route("logout")]
        [userAuth]
        public HttpResponseMessage logout()
        {
            string tokenString = Request.Headers.Authorization.ToString();
            if(authService.userLogout(tokenString))
                return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Successfully logout" });
            return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Logout b"});
        }
        [HttpPatch]
        [Route("changepassword")]
        [userAuth]
        public HttpResponseMessage changePassword(changePasswordDTO cpObj)
        {
            int userID = getID(Request);
            if(authService.changePassword(userID, cpObj.oldPassword, cpObj.newPassword))
                return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Password is changed successfully" });
            return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Password changing was unsuccessfull" });
        }
        [HttpGet]
        [Route("profile")]
        [userAuth]
        public HttpResponseMessage profile()
        {
            try
            {
                int userID = getID(Request);
                var data = userServices.getProfile(userID);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("notice/{noticeID}")]
        [userAuth]
        public HttpResponseMessage getNotice(int noticeID)
        {
            try
            {
                var data = userServices.getNotice(noticeID);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("notice")]
        [userAuth]
        public HttpResponseMessage getNotice()
        {
            try
            {
                var data = userServices.getNotice();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
