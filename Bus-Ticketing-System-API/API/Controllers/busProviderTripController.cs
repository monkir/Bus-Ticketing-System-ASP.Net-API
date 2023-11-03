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
    [RoutePrefix("api/busprovider/trip")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [busProviderAuth]
    public class busProviderTripController : ApiController
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
            int bp_id = getID(Request);
            var data = busProviderTripService.allTrip(bp_id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpGet]
        [Route("all/details")]
        public HttpResponseMessage allTripDetails()
        {
            int bp_id = getID(Request);
            var data = busProviderTripService.allTripDetails(bp_id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpGet]
        [Route("search/{search}")]
        public HttpResponseMessage searchTripDetails(string search)
        {
            int bp_id = getID(Request);
            var data = busProviderTripService.searchTripDetails(bp_id, search);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpGet]
        [Route("get/{id}")]
        public HttpResponseMessage findTrip(int id)
        {
            try
            {
                if (busProviderTripService.isOwnerOfTrip(id, getID(Request)) == false)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The busprovider is not owner of this trip" });
                }
                var data = busProviderTripService.GetTrip(id);
                //string message = data ? "bus is deleted" : "bus is not deleted";
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpGet]
        [Route("get/{id}/details")]
        public HttpResponseMessage findTripDetails(int id)
        {
            try
            {
                if (busProviderTripService.isOwnerOfTrip(id, getID(Request)) == false)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The busprovider is not owner of this trip" });
                }
                var data = busProviderTripService.getTripDetails(id);
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
                int busID = obj.bus_id;
                int bp_id = getID(Request);
                if (busProviderTripService.isOwnerOfBus(busID, bp_id) == false)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The busprovider is not owner of this trip" });
                }
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
        [Route("add/undo/{tripid}")]
        public HttpResponseMessage undoAddTrip(int tripID)
        {
            try
            {
                int bp_id = getID(Request);
                if(busProviderTripService.isOwnerOfTrip(tripID, bp_id) == false)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The busprovider is not owner of this trip" });
                }
                string tripStatus = busProviderTripService.GetTrip(tripID).status;
                if(tripStatus != "adding-pending")
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "This trip cannot be deleted" });
                }
                var data = busProviderTripService.undoAddTrip(tripID);
                string message = data ? "The trip is deleted" : "New trip is not deleted";
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
                int bp_id = getID(Request);
                if(busProviderTripService.isOwnerOfTrip(tripID, bp_id) == false)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The busprovider is not owner of this trip" });
                }
                string tripStatus = busProviderTripService.GetTrip(tripID).status;
                if(tripStatus == "cancelling-pending")
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

        [HttpPost]
        [Route("cancel/undo/{tripid}")]
        public HttpResponseMessage undoCancelTrip(int tripID)
        {
            try
            {
                int bp_id = getID(Request);
                if(busProviderTripService.isOwnerOfTrip(tripID, bp_id) == false)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The busprovider is not owner of this trip" });
                }
                string tripStatus = busProviderTripService.GetTrip(tripID).status;
                if(tripStatus == "added")
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The trip is already in added list" });
                }
                if(tripStatus != "cancelling-pending")
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "This trip is not in cancelling list" });
                }
                var data = busProviderTripService.undoCancelTrip(tripID);
                string message = data ? "The trip is requested to be cancelled" : "New trip is not cancelled";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("places")]
        public HttpResponseMessage allPlace()
        {
            var data = busProviderTripService.allPlace();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
    }
}
