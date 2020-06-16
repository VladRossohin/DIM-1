using System.Web.Mvc;
using System.Web.Routing;

namespace DIMS.Server
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
                defaults: new { controller = "TaskTrack", action = "Index", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Account",
                url: "{controller}/{action}/",
                defaults: new { controller = "Account", action = "Login" }
            );
        }
    }
}
