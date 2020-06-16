using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Interfaces;
using DIMS.Server.Models.Tasks;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DIMS.Server.Controllers.Menthor
{
    public abstract class BaseMVCController : Controller
    {
        protected readonly ITaskService _taskService;
        protected readonly IVTaskService _vTaskService;
        protected readonly IUserTaskService _userTaskService;
        protected readonly IVUserProfileService _vUserProfileService;
        protected readonly IVTaskStateService _vTaskStateService;
        protected readonly IVUserTaskService _vUserTaskService;
        protected readonly IMapper _mapper;

        public BaseMVCController(ITaskService taskService, 
            IVTaskService vTaskService, 
            IUserTaskService userTaskService, 
            IVUserProfileService vUserProfileService, 
            IVTaskStateService vTaskStateService, 
            IVUserTaskService vUserTaskService, 
            IMapper mapper)
        {
            _taskService = taskService;
            _vTaskService = vTaskService;
            _userTaskService = userTaskService;
            _vUserProfileService = vUserProfileService;
            _vTaskStateService = vTaskStateService;
            _vUserTaskService = vUserTaskService;
            _mapper = mapper;
        }

        public List<string> GetUsersForTask(int id)
        {
            var userTaskDtos = _userTaskService.GetAllUserTasksByTaskId(id);
            var userTasks = _mapper.Map<IEnumerable<UserTaskDTO>, List<UserTaskViewModel>>(userTaskDtos);

            var usersTaskList = new List<string>();

            foreach (var user in userTasks)
            {
                usersTaskList.Add(user.UserId.ToString());
            }

            return usersTaskList;
        }
    }
}