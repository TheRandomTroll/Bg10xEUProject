using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProjectMilky
{
    using AutoMapper;

    using ProjectMilky.Models;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Mapper.Initialize(
                expression =>
                    {
                        expression.CreateMap<ApplicationUser, Student>(); 
                        expression.CreateMap<ApplicationUser, Teacher>(); 
                        
                    });
        }
    }
}
