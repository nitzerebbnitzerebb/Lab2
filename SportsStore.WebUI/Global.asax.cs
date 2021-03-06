﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SportsStore.WebUI.Infrastructure;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Binders;

namespace SportsStore.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null,
                "", // Only matches the empty URL (i.e. /)
                new { controller = "Product", action = "List", category = (string)null, page = 1 }
            );

            routes.MapRoute(
                "ProductPaging", // Route name
                "Page{page}", // URL with parameters
                new { controller = "Product", action = "List"} // Parameter defaults
            );

            routes.MapRoute(
                "Categoryfiltering", // Route name
                "{category}", // URL with parameters
                new { controller = "Product", action = "List", page = 1 } // Parameter defaults
            );

            routes.MapRoute(
                "CategoryAndPage", // Route name
                "{category}/Page{page}", // URL with parameters
                new { controller = "Product", action = "List" }, // Parameter defaults
                new { page = @"\d+"}
            );

            routes.MapRoute(
                null,
                "{controller}/{action}"
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}