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

        public TasksController(ITaskService taskService, IvTaskService vTaskService, IUserTaskService userTaskService)
        {
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
            _vTaskService = vTaskService ?? throw new ArgumentNullException(nameof(vTaskService));
            _userTaskService = userTaskService ?? throw new ArgumentNullException(nameof(userTaskService));
        }

        [HttpPost]
        [Route("task/create")]
        public IHttpActionResult Create([FromBody]TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                var taskDto = Mapper.Map<TaskViewModel, TaskDTO>(task);
                _taskService.Save(taskDto);

                task.TaskId = _taskService.GetAll().LastOrDefault().TaskId;

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Created, Json(task)));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, Json(ModelState.Values)));
        }

        [HttpPost]
        [Route("task/add/{id}")]
        public IHttpActionResult AddUsers([FromUri]int? taskId, IEnumerable<int> userIds)
        {
            foreach(var userId in userIds)
            {
                try
                {
                    var userTask = new UserTaskDTO
                    {
                        StateId = 1, // default - In Progress
                        TaskId = taskId.Value,
                        UserId = userId
                    };

                    _userTaskService.Save(userTask);

                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "Task was succesfully created!"));

                } catch (ValidationException ex)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Please try again later"));
                }
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Something went wrong"));
        }

    }
}