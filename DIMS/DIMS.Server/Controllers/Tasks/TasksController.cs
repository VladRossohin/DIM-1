using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Interfaces;
using DIMS.Server.Models.Tasks;
using DIMS.Server.Models.Users;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DIMS.Server.Controllers.Tasks
{
    public class TasksController : Controller
    {
        private readonly IVTaskService _taskService;
        private readonly IVUserProfileService _userProfileService;
        private readonly IMapper _mapper;
        public TasksController(IVTaskService taskService, IVUserProfileService userProfileService, IMapper mapper)
        {
            _taskService = taskService;
            _userProfileService = userProfileService;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            var TasksViewModel = new TasksListViewModel
            {
                Tasks = _mapper.Map<IEnumerable<VTaskDTO>, IEnumerable<vTaskViewModel>>(
                    _taskService.GetAll()),

                UserProfiles = _mapper.Map<IEnumerable<VUserProfileDTO>, IEnumerable<VUserProfileViewModel>>(
                    _userProfileService.GetAll())
            };

            return View(TasksViewModel);
        }
    }
}