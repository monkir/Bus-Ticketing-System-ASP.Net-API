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
    [adminAuth]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class adminCuponController : ApiController
    {
        private int getID(HttpRequestMessage request)
        {
            string tokenString = request.Headers.Authorization.ToString();
            return authService.authorizeUser(tokenString).userid;
        }
        [HttpGet]
        [Route("api/admin/cupon/all")]
        public HttpResponseMessage allCupon()
        {
            var data = adminCuponService.allDiscountCupon();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpGet]
        [Route("api/admin/cupon/search")]
        public HttpResponseMessage searchCupon(string search)
        {
            search = search.ToLower();
            var data = adminCuponService.allDiscountCupon().Where(
                c =>
                    c.id.ToString().Contains(search)
                    && c.name.ToLower().Contains(search)
                    && c.cupon.ToLower().Contains(search)
                    && c.percentage.ToString().ToLower().Contains(search)
                    && c.maxDiscount.ToString().ToLower().Contains(search)
                    && c.admin_id.ToString().ToLower().Contains(search)
                );
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpPost]
        [Route("api/admin/cupon/add")]
        public HttpResponseMessage addCupon(discountCuponDTO obj)
        {
            try
            {
                obj.admin_id = getID(Request);
                var data = adminCuponService.addDiscountCupon(obj);
                string message = data ? "New cupon is created" : "New cupon is not created";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message});
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }

        [HttpPut]
        [Route("api/admin/cupon/update")]
        public HttpResponseMessage updateCupon(discountCuponDTO obj)
        {
            try
            {
                if (adminCuponService.GetDiscountCupon(obj.id) == null)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The cupon doesn't exits" });
                }
                obj.admin_id = getID(Request);
                var data = adminCuponService.updateDiscountCupon(obj);
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
                if(adminCuponService.GetDiscountCupon(id) == null)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The cupon doesn't exits" });
                }
                var data = adminCuponService.deleteDiscountCupon(id);
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
                if (adminCuponService.GetDiscountCupon(id) == null)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The cupon doesn't exits" });
                }
                var data = adminCuponService.GetDiscountCupon(id);
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
