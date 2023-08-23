using API.Auth;
using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Services.Description;

namespace API.Controllers
{
    [RoutePrefix("api/customer/ticket")]
    [customerAuth]
    public class customerTicketController : ApiController
    {
        private int getID(HttpRequestMessage request)
        {
            string tokenString = request.Headers.Authorization.ToString();
            return authService.authorizeUser(tokenString).userid;
        }
        [HttpPost]
        [Route("parchase")]
        public HttpResponseMessage purchase(ticketDTO ticket)
        {
            if(customerTicketService.isSeatAvailable(ticket.trip_id, ticket.seat_no) == false) 
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new {message = "Requested seat is not available"});
            }
            if(ticket.cupon != null)
            {
                ticket.dc_id = customerTicketService.getCuponIDbyString(ticket.cupon);
                if(ticket.dc_id == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { message = "Applied cupon is not valid" });
                }
            }
            else
            {
                ticket.dc_id = null;
            }
            ticket.cust_id = getID(Request);
            ticket.seat_no = ticket.seat_no.Distinct().ToList();
            var data = customerTicketService.purchaseTicket(ticket);
            var message = data ? "The ticket is booked" : "The ticket is not booked";
            return Request.CreateResponse(HttpStatusCode.OK, new { message = message});
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
