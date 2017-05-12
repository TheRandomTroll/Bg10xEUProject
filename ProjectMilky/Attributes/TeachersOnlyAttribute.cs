using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMilky.Attributes
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class TeachersOnlyAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var rd = httpContext.Request.RequestContext.RouteData;
            string currentAction = rd.GetRequiredString("action");
            string currentController = rd.GetRequiredString("controller");
            string currentArea = rd.Values["area"] as string;

            if (httpContext.User != null
                && httpContext.User.Identity != null
                && httpContext.User.Identity.IsAuthenticated
                && httpContext.User.IsInRole("Teacher")
                && currentController != "Home")
            {
                return true;
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                                   new RouteValueDictionary
                                   {
                                       { "action", "Index" },
                        { "controller", "Home" },
                        { "area", string.Empty }
                    });
        }
    }
}