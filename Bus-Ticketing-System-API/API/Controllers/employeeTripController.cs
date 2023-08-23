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
    [RoutePrefix("api/employee/trip")]
    [employeeAuth]
    public class employeeTripController : ApiController
    {
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage allTrip()
        {
            var data = employeeTripService.allTrip();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpGet]
        [Route("get/{id}")]
        public HttpResponseMessage findTrip(int id)
        {
            try
            {
                var data = employeeTripService.GetTrip(id);
                //string message = data ? "bus is deleted" : "bus is not deleted";
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPost]
        [Route("accept/add/{tripID}")]
        public HttpResponseMessage confirmAddTrip(int tripID)
        {
            try
            {
                //obj.bus_id = getID(Request);
                var data = employeeTripService.acceptAddTrip(tripID);
                string message = data ? "New trip is accepted to be added" : "Accepting new trip to be added is failed";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

        }
        [HttpPost]
        [Route("accept/cancel/{tripID}")]
        public HttpResponseMessage confirmCancelTrip(int tripID)
        {
            try
            {
                //obj.bus_id = getID(Request);
                var data = employeeTripService.acceptCancelTrip(tripID);
                string message = data ? "A trip is accepted to be cancelled" : "Accepting a trip to be cancelled is failed";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

        }
    }
}
