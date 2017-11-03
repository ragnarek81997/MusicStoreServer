using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace MusicStoreServer.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "signup",
                url: "signup",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "signin",
                url: "signin",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "forgotpassword",
                url: "forgotpassword",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Profile",
                url: "profile/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ResetPassword",
                url: "resetpassword",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "AccessDenied",
                url: "error/access-denied",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "NotFound",
                url: "error/not-found",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "ServerError",
                url: "error/server-error",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
