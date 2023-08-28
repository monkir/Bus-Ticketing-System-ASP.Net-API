using API.Auth;
using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [RoutePrefix("api/employee/notice")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [employeeAuth]
    public class employeeNoticeController : ApiController
    {
        private int getID(HttpRequestMessage request)
        {
            string tokenString = request.Headers.Authorization.ToString();
            return authService.authorizeUser(tokenString).userid;
        }
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage allNotice()
        {
            var data = employeeNoticeService.allNotice();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage addNotice(noticeDTO obj)
        {
            try
            {
                obj.emp_id = getID(Request);
                var data = employeeNoticeService.addNotice(obj);
                string message = data ? "New notice is created" : "New notice is not created";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        [HttpPut]
        [Route("update")]
        public HttpResponseMessage updateNotice(noticeDTO obj)
        {
            try
            {
                if (employeeNoticeService.GetNotice(obj.id) == null)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The bus provider doesn't exits" });
                }
                obj.emp_id = getID(Request);
                var data = employeeNoticeService.updateNotice(obj);
                string message = data ? "notice is updated" : "notice is not updated";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        [HttpDelete]
        [Route("delete/{id}")]
        public HttpResponseMessage deleteNotice(int id)
        {
            try
            {
                if (employeeNoticeService.GetNotice(id) == null)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The bus provider doesn't exits" });
                }
                var data = employeeNoticeService.deleteNotice(id);
                string message = data ? "notice is deleted" : "notice is not deleted";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        [HttpGet]
        [Route("get/{id}")]
        public HttpResponseMessage findNotice(int id)
        {
            try
            {
                if (employeeNoticeService.GetNotice(id) == null)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The bus provider doesn't exits" });
                }
                var data = employeeNoticeService.GetNotice(id);
                //string message = data ? "notice is deleted" : "notice is not deleted";
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
