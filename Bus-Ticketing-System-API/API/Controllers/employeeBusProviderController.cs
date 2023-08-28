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
    [RoutePrefix("api/employee/busprovider")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [employeeAuth]
    public class employeeBusProviderController : ApiController
    {
        private int getID(HttpRequestMessage request)
        {
            string tokenString = request.Headers.Authorization.ToString();
            return authService.authorizeUser(tokenString).userid;
        }
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

                if (userServices.usernameExist(obj.username))
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The user id is already exist" });
                }
                obj.emp_id = getID(Request);
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

                if (userServices.usernameExist(obj.username))
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The user id is already exist" });
                }
                if (employeeBusProviderService.getBusProvider(obj.id) == null)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The bus provider doesn't exits" });
                }
                obj.emp_id = getID(Request);
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
                if (employeeBusProviderService.getBusProvider(id) == null)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The bus provider doesn't exits" });
                }
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
