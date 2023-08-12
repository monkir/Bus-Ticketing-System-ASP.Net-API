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
    public class adminController : ApiController
    {
        [HttpGet]
        [Route("api/admin/cupon/all")]
        public HttpResponseMessage allCupon()
        {
            var data = adminServices.allDiscountCupon();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpPost]
        [Route("api/admin/cupon/add")]
        public HttpResponseMessage addCupon(discountCuponDTO obj)
        {
            try
            {
                var data = adminServices.addDiscountCupon(obj);
                string message = data ? "New cupon is created" : "New cupon is not created";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message});
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }
        [HttpPost]
        [Route("api/admin/cupon/update")]
        public HttpResponseMessage updateCupon(discountCuponDTO obj)
        {
            try
            {
                var data = adminServices.updateDiscountCupon(obj);
                string message = data ? "Cupon is updated" : "Cupon is not updated";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message});
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }
        [HttpDelete]
        [Route("api/admin/cupon/delete/{id}")]
        public HttpResponseMessage deleteCupon(int id)
        {
            try
            {
                var data = adminServices.deleteDiscountCupon(id);
                string message = data ? "Cupon is deleted" : "Cupon is not deleted";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message});
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }
        [HttpGet]
        [Route("api/admin/cupon/get/{id}")]
        public HttpResponseMessage findCupon(int id)
        {
            try
            {
                var data = adminServices.GetDiscountCupon(id);
                //string message = data ? "Cupon is deleted" : "Cupon is not deleted";
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }
    }
}
