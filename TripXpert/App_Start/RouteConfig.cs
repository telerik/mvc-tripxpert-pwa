using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TripXpert
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Home",
                "Home",
                new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                "About",
                "About",
                new { controller = "About", action = "About" }
            );

            routes.MapRoute(
               "Attractions",
               "Attractions",
               new { controller = "Attractions", action = "Attractions" }
           );

            routes.MapRoute(
               "Destinations",
               "Destinations",
               new { controller = "Destinations", action = "Destinations" }
           );
            routes.MapRoute(
               "DestinationDetails",
               "Destination/{id}",
               new { controller = "Destinations", action = "DestinationDetails", id =UrlParameter.Optional }
           );

            routes.MapRoute(
               "Contact",
               "Contact",
               new { controller = "Contact", action = "Contact" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
