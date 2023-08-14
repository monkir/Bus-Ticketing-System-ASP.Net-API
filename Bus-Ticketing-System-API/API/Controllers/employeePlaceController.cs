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
    public class employeePlaceController : ApiController
    {
        [HttpGet]
        [Route("api/employee/place/all")]
        public HttpResponseMessage allPlace()
        {
            var data = employeePlaceService.allPlace();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpPost]
        [Route("api/employee/place/add")]
        public HttpResponseMessage addPlace(placeDTO obj)
        {
            try
            {
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
        [Route("api/employee/place/update")]
        public HttpResponseMessage updatePlace(placeDTO obj)
        {
            try
            {
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
        [Route("api/employee/place/delete/{id}")]
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
        [Route("api/employee/place/get/{id}")]
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
