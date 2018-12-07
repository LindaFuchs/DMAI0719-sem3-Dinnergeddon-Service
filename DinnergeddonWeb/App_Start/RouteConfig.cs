using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DinnergeddonWeb {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
    "HomeActions",
    "Download",
    new { controller = "Home", action = "Download" }
);


            routes.MapRoute("ContactRoute", "{type}",
       new { controller = "Home", action = "Contact", id = UrlParameter.Optional },
       new RouteValueDictionary
       {
            { "type", "Contact|Contact-us|Contact_us" }
       });

            routes.MapRoute("AboutRoute", "{type}",
      new { controller = "Home", action = "About", id = UrlParameter.Optional },
      new RouteValueDictionary
      {
            { "type", "about|about-us|about_us" }
      });


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

           // routes.MapRoute(
           //    name: "WithoutHome",
           //     url: "{action}/{id}",
           //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           //);
        }
    }
}
