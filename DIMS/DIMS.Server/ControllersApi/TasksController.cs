using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Infrastructure;
using DIMS.BL.Interfaces;
using DIMS.Server.Models;
using DIMS.Server.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DIMS.Server.ControllersApi
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api")]
    public class TasksController : ApiController
    {
        private readonly ITaskService _taskService;
        private readonly IVTaskService _vTaskService;
        private readonly IUserTaskService _userTaskService;
        private readonly IVUserTaskService _vUserTaskService;

        private readonly IMapper _mapper;

        public TasksController(ITaskService taskService, IVTaskService vTaskService, IUserTaskService userTaskService, IVUserTaskService vUserTaskService, IMapper mapper)
        {
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
            _vTaskService = vTaskService ?? throw new ArgumentNullException(nameof(vTaskService));
            _userTaskService = userTaskService ?? throw new ArgumentNullException(nameof(userTaskService));
            _vUserTaskService = vUserTaskService ?? throw new ArgumentNullException(nameof(vUserTaskService));
            _mapper = mapper;
        }

        [HttpDelete]
        [Route("task/delete/{id}")]
        public IHttpActionResult Delete([FromUri]int? id)
        {

            if (id.HasValue)
            {
                _taskService.DeleteById(id.Value);
            }

            else
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "The task id value is not set!"));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, $"The task with id = {id.Value} has been succesfully deleted!"));
        }

        [HttpGet]
        [Route("tasks")]
        public IHttpActionResult GetTasks()
        {
            var taskDtos = _vTaskService.GetAll();

            var tasks = _mapper.Map<IEnumerable<VTaskDTO>, IEnumerable<TaskViewModel>>(taskDtos);

            return Json(tasks);
        }

        [HttpGet]
        [Route("task/{id?}")]
        public IHttpActionResult GetTask([FromUri]int? id)
        {
            if (id.HasValue)
            {
                var taskDto = _taskService.GetById(id.Value);

                if (taskDto != null)
                {
                    var task = _mapper.Map<TaskDTO, TaskViewModel>(taskDto);

                    return Json(task);
                }

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, $"There's no task with id = {id.Value}"));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Please set task id!"));
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

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, Json("There was an error occured creating task, please try again later!")));
        }

        [HttpPut]
        [Route("task/edit")]
        public IHttpActionResult EditTask([FromBody]TaskViewModel task)
        {
            if (task != null)
            {
                var taskDto = _taskService.GetById(task.TaskId);

                if (taskDto == null)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, $"There's no task with id = {task.TaskId}"));
                }

                taskDto.Name = task.Name;
                taskDto.Description = task.Description;
                taskDto.StartDate = task.StartDate;
                taskDto.DeadlineDate = task.DeadlineDate;

                _taskService.Update(taskDto);

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "The task has been succesfully updated!"));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Task was empty, please send request with all task fields!"));
        }


    }
}