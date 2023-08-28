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
    [RoutePrefix("api/busprovider/bus")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [busProviderAuth]
    public class busProviderBusController : ApiController
    {
        private int getID(HttpRequestMessage request)
        {
            string tokenString = request.Headers.Authorization.ToString();
            return authService.authorizeUser(tokenString).userid;
        }
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage allBus()
        {
            int bp_id = getID(Request);
            var data = busProviderBusService.allBus(bp_id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage addBus(busDTO obj)
        {
            try
            {
                obj.bp_id = getID(Request);
                var data = busProviderBusService.addBus(obj);
                string message = data ? "New bus is created" : "New bus is not created";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        [HttpPut]
        [Route("update")]
        public HttpResponseMessage updateBus(busDTO obj)
        {
            try
            {
                if (busProviderBusService.isOwner(obj.id, getID(Request)) == false)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The busprovider is not owner of this bus" });
                }
                obj.bp_id = getID(Request);
                var data = busProviderBusService.updateBus(obj);
                string message = data ? "bus is updated" : "bus is not updated";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        [HttpDelete]
        [Route("delete/{id}")]
        public HttpResponseMessage deleteBus(int id)
        {
            try
            {
                if (busProviderBusService.isOwner(id, getID(Request)) == false)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The busprovider is not owner of this bus" });
                }
                var data = busProviderBusService.deleteBus(id);
                string message = data ? "bus is deleted" : "bus is not deleted";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        [HttpGet]
        [Route("get/{id}")]
        public HttpResponseMessage findBus(int id)
        {
            try
            {
                if (busProviderBusService.isOwner(id, getID(Request)) == false)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The busprovider is not owner of this bus" });
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
        [HttpGet]
        [Route("get/{id}/trip")]
        public HttpResponseMessage triplistByBus(int id)
        {
            try
            {
                if (busProviderBusService.isOwner(id, getID(Request)) == false)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The busprovider is not owner of this bus" });
                }
                var data = busProviderBusService.tripList_of_bus(id);
                //string message = data ? "bus is deleted" : "bus is not deleted";
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
