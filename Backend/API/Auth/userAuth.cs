using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace API.Auth
{
    public class userAuth:AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authToken = actionContext.Request.Headers.Authorization;
            if (authToken == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(
                        System.Net.HttpStatusCode.Unauthorized,
                        new { Message = "No token is supllied in header" }
                    );
            }
            else
            {
                var exToken = authService.authorizeUser(authToken.ToString());
                if (exToken == null)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(
                            System.Net.HttpStatusCode.Unauthorized,
                            new { Message = "Supplied token is not valid" }
                        );
                }
                else if (exToken.expireTime.CompareTo(DateTime.Now) < 0)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(
                            System.Net.HttpStatusCode.Unauthorized,
                            new { Message = "Supplied token is expired" }
                        );
                }
                /*else
                {
                    var exUser = authService.getUserByTokenID(exToken.id);
                    if (exUser.userRole.Equals("employee") == false)
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(
                            System.Net.HttpStatusCode.Forbidden,
                            new { Message = "Only employee can access here" }
                        );
                    }
                }*/
            }
            base.OnAuthorization(actionContext);
        }
    }
}