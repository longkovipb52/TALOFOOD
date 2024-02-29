using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LTW_FFOOD
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional }
            );
            routes.MapRoute(
    name: "AddToCart",
    url: "ShoppingCart/addtocart/{id}",
    defaults: new { controller = "ShoppingCart", action = "addtocart" }
);
            routes.MapRoute(
              name: "Checkout",
              url: "ShoppingCart/Checkout",
              defaults: new { controller = "ShoppingCart", action = "Checkout" }
          );
        }

    }
}
