using DIMS.Server.Models;
using DIMS.Server.Controllers.Users;
using DIMS.Server.Controllers.Menthor;
using System.Web.Mvc;

namespace DIMS.Server.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index(MainPageViewModel mainPageViewModel)
        {
            var userProfiles = new ActionLinkViewModel
            {
                ButtonText = "User Profiles",
                ActionName = "Index",
                ControllerName = "UserProfile",
                ButtonType = "secondary"
            };

            var tasksManage = new ActionLinkViewModel
            {
                ButtonText = "Tasks Manage Grid",
                ActionName = "Index",
                ControllerName = "TasksManage",
                ButtonType = "secondary"
            };

            if (User.IsInRole("user"))
            {
                mainPageViewModel.CoverHead = "Hi, Mr.User";
                mainPageViewModel.ActionLinks.Add(new ActionLinkViewModel
                {
                    ButtonText = "My Tasks",
                    ActionName = "Index",
                    ControllerName = "UserTask",
                    LinkId = 2006,
                    ButtonType = "secondary"
                });
                mainPageViewModel.ActionLinks.Add(new ActionLinkViewModel
                {
                    ButtonText = "Task Tracks Grid",
                    ActionName = "Index",
                    ControllerName = "TaskTrack",
                    LinkId = 2006,
                    ButtonType = "secondary"
                });
            }
            else if (User.IsInRole("mentor"))
            {
                mainPageViewModel.CoverHead = "Hi, Mr.Mentor";
                mainPageViewModel.ActionLinks.Add(userProfiles);
                mainPageViewModel.ActionLinks.Add(tasksManage);
            }
            else if (User.IsInRole("admin"))
            {
                mainPageViewModel.CoverHead = "Hi, Mr.Admin";
                mainPageViewModel.ActionLinks.Add(userProfiles);
                mainPageViewModel.ActionLinks.Add(tasksManage);
            }
            else
            {
                mainPageViewModel.CoverHead = "Welcome to DevIncubator!";
                mainPageViewModel.ActionLinks.Add(new ActionLinkViewModel
                {
                    ButtonText = "Login",
                    ActionName = "Login",
                    ControllerName = "Account",
                    ButtonType = "secondary"
                });
            }
            if (User.Identity.IsAuthenticated)
            {
                mainPageViewModel.ActionLinks.Add(new ActionLinkViewModel
                {
                    ButtonText = "Logout",
                    ActionName = "Logout",
                    ControllerName = "Account",
                    ButtonType = "danger"
                });
            }
            return View(mainPageViewModel);
        }
    }
}