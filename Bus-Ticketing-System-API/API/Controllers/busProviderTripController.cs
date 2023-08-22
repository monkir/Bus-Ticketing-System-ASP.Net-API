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
    [RoutePrefix("api/busprovider/trip")]
    [busProviderAuth]
    public class busProviderTripController : ApiController
    {
        private int getID(HttpRequestMessage request)
        {
            string tokenString = request.Headers.Authorization.ToString();
            return authService.authorizeUser(tokenString).id;
        }
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage allTrip()
        {
            int bp_id = getID(Request);
            var data = busProviderTripService.allTrip(bp_id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpGet]
        [Route("get/{id}")]
        public HttpResponseMessage findTrip(int id)
        {
            try
            {
                if (busProviderTripService.isOwner(id, getID(Request)) == false)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The busprovider is not owner of this trip" });
                }
                var data = busProviderBusService.GetBus(id);
                //string message = data ? "bus is deleted" : "bus is not deleted";
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage addTrip(tripDTO obj)
        {
            try
            {
                obj.bus_id = getID(Request);
                var data = busProviderTripService.addTrip(obj);
                string message = data ? "New trip is requested to be added" : "New trip is not added";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

        }

        [HttpDelete]
        [Route("delete/{tripid}")]
        public HttpResponseMessage deleteTrip(int tripID)
        {
            try
            {
                int bus_id = getID(Request);
                if(busProviderTripService.isOwner(tripID, bus_id) == false)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The busprovider is not owner of this trip" });
                }
                string tripStatus = busProviderTripService.GetTrip(tripID).status;
                if(tripStatus != "adding/pending")
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "This trip cannot be deleted" });
                }
                var data = busProviderTripService.cancelTrip(tripID);
                string message = data ? "The trip is requested to be cancelled" : "New trip is not cancelled";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("cancel/{tripid}")]
        public HttpResponseMessage cancelTrip(int tripID)
        {
            try
            {
                int bus_id = getID(Request);
                if(busProviderTripService.isOwner(tripID, bus_id) == false)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The busprovider is not owner of this trip" });
                }
                string tripStatus = busProviderTripService.GetTrip(tripID).status;
                if(tripStatus == "cancelling/pending")
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "This trip is already requested to be cancalled" });
                }
                if(tripStatus != "added")
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "This trip cannot be cancalled" });
                }
                var data = busProviderTripService.cancelTrip(tripID);
                string message = data ? "The trip is requested to be cancelled" : "New trip is not cancelled";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
