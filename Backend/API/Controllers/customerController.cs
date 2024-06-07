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
    [RoutePrefix("api/customer")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class customerController : ApiController
    {
        [HttpPost]
        [Route("registration")]
        public HttpResponseMessage registration(customerDTO custObj)
        {
            try
            {
                if(userServices.usernameExist(custObj.username))
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The user id is already exist" });
                }
                var data = customerUserService.resgirstration(custObj);
                if(data == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { message = "Registraion is successful" });
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = "Registraion is unsuccessful" });
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { message = ex.Message });
            }
        }
    }
}
