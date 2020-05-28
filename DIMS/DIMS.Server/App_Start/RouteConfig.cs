﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HIMS.Server
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "UserProfile",
                url: "users/{action}/",
                defaults: new { controller = "UserProfile", action = "Index", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "TasksManage",
                url: "tasks/{action}/",
                defaults: new { controller = "TasksManage", action = "Index", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "TaskTrack",
                url: "track/{action}/",
                defaults: new { controller = "TaskTrack", action = "Index", id = 2006 }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );



        }
    }
}
