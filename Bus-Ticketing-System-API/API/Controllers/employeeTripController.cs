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
    [RoutePrefix("api/employee/trip")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [employeeAuth]
    public class employeeTripController : ApiController
    {
        private int getID(HttpRequestMessage request)
        {
            string tokenString = request.Headers.Authorization.ToString();
            return authService.authorizeUser(tokenString).userid;
        }
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage allTrip()
        {
            var data = employeeTripService.allTrip();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpGet]
        [Route("all/details")]
        public HttpResponseMessage allTripDetails()
        {
            var data = employeeTripService.allTripDetails();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpGet]
        [Route("search/{search}")]
        public HttpResponseMessage searchTrip(string search)
        {
            var data = employeeTripService.searchTrip(search);
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
                var tripObj = employeeTripService.GetTrip(tripID);
                if(tripObj == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "This trip is not founded" });
                }
                if (tripObj.status == "added")
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "This trip is already added" });
                }
                if (tripObj.status != "adding-pending")
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "This trip cannot be added" });
                }
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
        [Route("done/{tripID}")]
        public HttpResponseMessage doneTrip(int tripID)
        {
            try
            {
                var tripObj = employeeTripService.GetTrip(tripID);
                if(tripObj == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "This trip is not founded" });
                }
                if (tripObj.status == "done")
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "This trip is already done" });
                }
                if (tripObj.status != "added")
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "This trip cannot be done" });
                }
                //obj.bus_id = getID(Request);
                var data = employeeTripService.doneTrip(tripID);
                string message = data ? "Trip has been done successfully" : "Trip has not been done unsuccessfully";
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

                var tripObj = employeeTripService.GetTrip(tripID);
                if (tripObj == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "This trip is not founded" });
                }
                if (tripObj.status == "cancelled")
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "This trip is already cancelled" });
                }
                if (tripObj.status != "cancelling-pending")
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "This trip cannot be cancelled" });
                }
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
