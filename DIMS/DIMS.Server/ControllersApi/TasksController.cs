using AutoMapper;
using HIMS.BL.DTO;
using HIMS.BL.Infrastructure;
using HIMS.BL.Interfaces;
using HIMS.Server.Models;
using HIMS.Server.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HIMS.Server.ControllersApi
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api")]
    public class TasksController : ApiController
    {
        private readonly ITaskService _taskService;
        private readonly IvTaskService _vTaskService;
        private readonly IUserTaskService _userTaskService;
        private readonly IvUserTaskService _vUserTaskService;

        public TasksController(ITaskService taskService, IvTaskService vTaskService, IUserTaskService userTaskService, IvUserTaskService vUserTaskService)
        {
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
            _vTaskService = vTaskService ?? throw new ArgumentNullException(nameof(vTaskService));
            _userTaskService = userTaskService ?? throw new ArgumentNullException(nameof(userTaskService));
            _vUserTaskService = vUserTaskService ?? throw new ArgumentNullException(nameof(vUserTaskService));
        }

        [HttpGet]
        [Route("user/tasks/{id}")]
        public IHttpActionResult GetVUserTasks([FromUri]int? id)
        {
            if (id.HasValue)
            {
                var vUserTaskDtos = _vUserTaskService.GetByUserId(id.Value);

                var vUserTasks = Mapper.Map<IEnumerable<vUserTaskDTO>, IEnumerable<vUserTaskViewModel>>(vUserTaskDtos);

                return Json(vUserTasks);
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Please set user id!"));
        }


        [HttpGet]
        [Route("tasks")]
        public IHttpActionResult GetTasks()
        {
            var taskDtos = _vTaskService.GetAll();

            var tasks = Mapper.Map<IEnumerable<vTaskDTO>, IEnumerable<TaskViewModel>>(taskDtos);

            return Json(tasks);
        }

        [HttpPost]
        [Route("task/create")]
        public IHttpActionResult Create([FromBody]TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                var taskDto = new TaskDTO
                {
                    Name = task.Name,
                    Description = task.Description,
                    StartDate = task.StartDate,
                    DeadlineDate = task.DeadlineDate
                };

                _taskService.Save(taskDto);

                task.TaskId = _taskService.GetAll().LastOrDefault().TaskId;

                return Json<TaskViewModel>(task);
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, Json("Error")));
        }

        [HttpPost]
        [Route("user/task/add/{id}")]
        public IHttpActionResult AddUsers([FromUri]int? id, [FromBody]IEnumerable<int> userIds)
        {
            try
            {
                foreach (var userId in userIds)
                {
                    var userTask = new UserTaskDTO
                    {
                        StateId = 1, // default - In Progress
                        TaskId = id.Value,
                        UserId = userId
                    };

                    _userTaskService.Save(userTask);
                }

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "Task was succesfully created!"));

            }
            catch (ValidationException ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Please try again later"));
            }

        }

        
    }
}