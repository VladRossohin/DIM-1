using AutoMapper;
using HIMS.BL.DTO;
using HIMS.BL.Interfaces;
using HIMS.Server.Models.Tasks;
using HIMS.Server.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HIMS.Server.Controllers.Tasks
{
    public class TasksController : Controller
    {
        private readonly IvTaskService _taskService;
        private readonly IvUserProfileService _userProfileService;

        public TasksController(IvTaskService taskService, IvUserProfileService userProfileService)
        {
            _taskService = taskService;
            _userProfileService = userProfileService;
        }

        public ActionResult Index()
        {
            var TasksViewModel = new TasksListViewModel
            {
                Tasks = Mapper.Map<IEnumerable<vTaskDTO>, IEnumerable<TaskViewModel>>(
                    _taskService.GetAll()),
                UserProfiles = Mapper.Map<IEnumerable<vUserProfileDTO>, IEnumerable<vUserProfileViewModel>>(
                    _userProfileService.GetAll())
            };

            return View(TasksViewModel);
        }


    }
}