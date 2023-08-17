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
    [RoutePrefix("api/employee/busprovider")]
    public class employeeBusProviderController : ApiController
    {
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage allBusProvider()
        {
            var data = employeeBusProviderService.allBusProvider();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage addBusProvider(busProviderDTO obj)
        {
            try
            {
                var data = employeeBusProviderService.addBusProvider(obj);
                string message = data ? "New busprovider is added" : "New busprovider is not added";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPut]
        [Route("update")]
        public HttpResponseMessage updateBusProvider(busProviderDTO obj)
        {
            try
            {
                var data = employeeBusProviderService.updateBusProvider(obj);
                string message = data ? "The busprovider data is updated" : "The busprovider data is not updated";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public HttpResponseMessage deleteBusProvider(int id)
        {
            try
            {
                var data = employeeBusProviderService.deleteBusProvider(id);
                string message = data ? "The busprovider data is deleted" : "The busprovider data is not deleted";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpGet]
        [Route("get/{id}")]
        public HttpResponseMessage getBusProvider(int id)
        {
            try
            {
                var data = employeeBusProviderService.getBusProvider(id);
                if (data == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "The user is not founded" });
                else
                    return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
