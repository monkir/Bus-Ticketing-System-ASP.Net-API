using API.Auth;
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
    [RoutePrefix("api/customer")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [customerAuth]
    public class customerTripAndCuponController : ApiController
    {
        private int getID(HttpRequestMessage request)
        {
            string tokenString = request.Headers.Authorization.ToString();
            return authService.authorizeUser(tokenString).userid;
        }
        [HttpGet]
        [Route("trip")]
        public HttpResponseMessage TripInDetails()
        {
            try
            {
                var data = customerTicketService.getTripInDetails();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }
        }
        [HttpGet]
        [Route("trip/my")]
        public HttpResponseMessage myTripInDetails()
        {
            try
            {
                int cust_id = getID(Request);
                var data = customerTicketService.getMyTripInDetails(cust_id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }
        }
        [HttpGet]
        [Route("trip/{tripid}")]
        public HttpResponseMessage TripInDetails(int tripid)
        {
            try
            {
                var data = customerTicketService.getTripInDetails(tripid);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }
        }
        [HttpGet]
        [Route("cupon")]
        public HttpResponseMessage CuponDetails()
        {
            try
            {
                var data = customerTicketService.getCuponDetails();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }
        }
        [HttpGet]
        [Route("cupon/{cuponid}")]
        public HttpResponseMessage CuponDetails(int cuponid)
        {
            try
            {
                var data = customerTicketService.getCuponDetails(cuponid);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
