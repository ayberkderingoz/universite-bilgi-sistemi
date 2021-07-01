using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OBS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Register",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Register", action = "Register", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Login",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Grades",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Grades", action = "Grades", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Info",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Info", action = "Info", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Teacher",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Teacher", action = "Teacher", id = UrlParameter.Optional }
            );
                routes.MapRoute(
                name: "Officer",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Register", action = "Officer", id = UrlParameter.Optional }
            );
        
        }
    }
}
