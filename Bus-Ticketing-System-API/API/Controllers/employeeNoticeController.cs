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
    public class employeeNoticeController : ApiController
    {
        [HttpGet]
        [Route("api/employee/notice/all")]
        public HttpResponseMessage allNotice()
        {
            var data = employeeNoticeService.allNotice();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpPost]
        [Route("api/employee/notice/add")]
        public HttpResponseMessage addNotice(noticeDTO obj)
        {
            try
            {
                var data = employeeNoticeService.addNotice(obj);
                string message = data ? "New notice is created" : "New notice is not created";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        [HttpPost]
        [Route("api/employee/notice/update")]
        public HttpResponseMessage updateNotice(noticeDTO obj)
        {
            try
            {
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
        [Route("api/employee/notice/delete/{id}")]
        public HttpResponseMessage deleteNotice(int id)
        {
            try
            {
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
        [Route("api/employee/notice/get/{id}")]
        public HttpResponseMessage findNotice(int id)
        {
            try
            {
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
