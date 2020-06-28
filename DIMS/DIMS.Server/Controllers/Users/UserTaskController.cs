using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Interfaces;
using DIMS.Server.Models.Tasks;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace DIMS.Server.Controllers.Users
{
    [Authorize]
    [RoutePrefix("usertask")]
    public class UserTaskController : Controller
    {
        private readonly ITaskTrackService _taskTrackService;
        private readonly IUserTaskService _userTaskService;
        private readonly IVUserTaskService _vUserTaskService;
        private readonly IVUserProfileService _vUserProfileService;
        private readonly IMapper _mapper;

        public UserTaskController(ITaskTrackService taskTrackService,
            IUserTaskService userTaskService,
            IVUserTaskService vUserTaskService,
            IVUserProfileService vUserProfileService,
        IMapper mapper)
        {
            _taskTrackService = taskTrackService;
            _userTaskService = userTaskService;
            _vUserTaskService = vUserTaskService;
            _vUserProfileService = vUserProfileService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult Index(int id)
        {
            var vUserDTO = _vUserProfileService.GetById(id);

            ViewBag.UserName = vUserDTO.FullName;
            ViewBag.UserId = vUserDTO.UserId;

            var vUserTaskDTOs = _vUserTaskService.GetByUserId(id);

            var vUserTasksViewModel = _mapper.Map<IEnumerable<VUserTaskDTO>, IEnumerable<VUserTaskViewModel>>(vUserTaskDTOs);

            return View(vUserTasksViewModel);
        }

        [HttpGet]
        [Route("details/{userId}/{taskId}")]
        public ActionResult Details(int userId, int taskId)
        {
            var vUserTaskDTO = _vUserTaskService.GetByUserId(userId).Where(usertask => usertask.TaskId == taskId).FirstOrDefault();

            if (vUserTaskDTO == null)
            {
                return HttpNotFound();
            }

            var vUserTaskViewModel = _mapper.Map<VUserTaskDTO, VUserTaskViewModel>(vUserTaskDTO);

            return View(vUserTaskViewModel);
        }

        [HttpGet]
        [Route("track/{userId}/{taskId}")]
        public ActionResult Create(int userId, int taskId)
        {
            ViewBag.UserId = userId;

            return View();
        }

        [HttpPost]
        [Route("track/{userId}/{taskId}")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskTrackViewModel taskTrackViewModel, int userId, int taskId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var taskTrackDTO = _mapper.Map<TaskTrackViewModel, TaskTrackDTO>(taskTrackViewModel);

                    var userTaskDTOs = _userTaskService.GetAllUserTasksByTaskId(taskId);

                    foreach (var item in userTaskDTOs)
                    {
                        if (item.UserId == userId)
                        {
                            var userTaskDTO = item;

                            taskTrackDTO.UserTaskId = userTaskDTO.UserTaskId;
                        }
                    }

                    _taskTrackService.Save(taskTrackDTO);
                    return RedirectToAction("Index", new { id = userId });
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(taskTrackViewModel);
        }
    }
}