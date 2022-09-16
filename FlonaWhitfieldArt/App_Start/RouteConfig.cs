using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FlonaWhitfieldArt
{
    public class RouteConfig
    {
        public static void Header(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Header",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "SiteLayout", action = "RenderHeader", id = UrlParameter.Optional }
            );
        }
        public static void Footer(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Footer",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "SiteLayout", action = "RenderFooter", id = UrlParameter.Optional }
            );
        }
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
