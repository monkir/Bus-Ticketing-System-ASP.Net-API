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
        [HttpPut]
        [Route("api/admin/employee/update")]
        public HttpResponseMessage updateEmployee(employeeDTO obj) 
        {
            try
            {
                var data = adminEmployeeService.updateEmployee(obj);
                string message = data ? "The employee data is updated" : "The employee data is not updated";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpDelete]
        [Route("api/admin/employee/delete/{id}")]
        public HttpResponseMessage deleteEmployee(int id) 
        {
            try
            {
                var data = adminEmployeeService.deleteEmployee(id);
                string message = data ? "The employee data is deleted" : "The employee data is not deleted";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = message });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpGet]
        [Route("api/admin/employee/get/{id}")]
        public HttpResponseMessage getEmployee(int id) 
        {
            try
            {
                var data = adminEmployeeService.getEmployee(id);
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
