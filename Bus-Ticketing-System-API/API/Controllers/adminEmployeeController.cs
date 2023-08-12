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
    public class adminEmployeeController : ApiController
    {
        [HttpGet]
        [Route("api/admin/employee/all")]
        public HttpResponseMessage allEmployee()
        {
            var data = adminEmployeeService.allEmployee();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpPost]
        [Route("api/admin/employee/add")]
        public HttpResponseMessage addEmployee(employeeDTO obj) 
        {
            try
            {
                var data = adminEmployeeService.addEmpoloyee(obj);
                string message = data ? "New employee is added" : "New employee is not added";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
