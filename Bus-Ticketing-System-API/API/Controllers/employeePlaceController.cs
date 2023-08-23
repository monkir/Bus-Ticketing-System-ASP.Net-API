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
    [RoutePrefix("api/employee/place")]
    [employeeAuth]
    public class employeePlaceController : ApiController
    {
        private int getID(HttpRequestMessage request)
        {
            string tokenString = request.Headers.Authorization.ToString();
            return authService.authorizeUser(tokenString).userid;
        }
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage allPlace()
        {
            var data = employeePlaceService.allPlace();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage addPlace(placeDTO obj)
        {
            try
            {
                obj.emp_id = getID(Request);
                var data = employeePlaceService.addPlace(obj);
                string message = data ? "New place is created" : "New place is not created";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        [HttpPost]
        [Route("update")]
        public HttpResponseMessage updatePlace(placeDTO obj)
        {
            try
            {
                obj.emp_id = getID(Request);
                var data = employeePlaceService.updatePlace(obj);
                string message = data ? "place is updated" : "place is not updated";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        [HttpDelete]
        [Route("delete/{id}")]
        public HttpResponseMessage deletePlace(int id)
        {
            try
            {
                var data = employeePlaceService.deletePlace(id);
                string message = data ? "place is deleted" : "place is not deleted";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        [HttpGet]
        [Route("get/{id}")]
        public HttpResponseMessage findPlace(int id)
        {
            try
            {
                var data = employeePlaceService.GetPlace(id);
                //string message = data ? "place is deleted" : "place is not deleted";
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
