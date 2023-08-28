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
    [RoutePrefix("api/admin/employee")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class adminEmployeeController : ApiController
    {
        private int getID(HttpRequestMessage request)
        {
            string tokenString = request.Headers.Authorization.ToString();
            return authService.authorizeUser(tokenString).userid;
        }
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage allEmployee()
        {
            var data = adminEmployeeService.allEmployee();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpGet]
        [Route("search/{search}")]
        public HttpResponseMessage searchEmployee(string search)
        {
            var data = adminEmployeeService.searchEmployee(search);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpPut]
        [Route("add")]
        public HttpResponseMessage addEmployee(employeeDTO obj) 
        {
            try
            {

                if (userServices.usernameExist(obj.username))
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The user id is already exist" });
                }
                obj.admin_id = getID(Request);
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
        [Route("update")]
        public HttpResponseMessage updateEmployee(employeeDTO obj) 
        {
            try
            {

                if (userServices.usernameExist(obj.username))
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The user id is already exist" });
                }
                if (adminEmployeeService.getEmployee(obj.id) == null)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The cupon doesn't exits" });
                }
                obj.admin_id = getID(Request);
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
        [Route("delete/{id}")]
        public HttpResponseMessage deleteEmployee(int id) 
        {
            try
            {
                if (adminEmployeeService.getEmployee(id) == null)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { message = "The cupon doesn't exits" });
                }
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
        [Route("get/{id}")]
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
