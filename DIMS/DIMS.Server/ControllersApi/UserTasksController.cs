using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Interfaces;
using DIMS.Server.Models.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class UserTasksController : ApiController
    {
        private readonly IUserTaskService _userTaskService;
        private readonly IVUserTaskService _vUserTaskService;

        private readonly IMapper _mapper;

        public UserTasksController(IUserTaskService userTaskService, IVUserTaskService vUserTaskService, IMapper mapper)
        {
            _userTaskService = userTaskService;
            _vUserTaskService = vUserTaskService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("user/tasks/{id}")]
        public IHttpActionResult GetVUserTasks([FromUri]int? id)
        {
            if (id.HasValue)
            {
                var vUserTaskDtos = _vUserTaskService.GetByUserId(id.Value);

                var vUserTasks = _mapper.Map<IEnumerable<VUserTaskDTO>, IEnumerable<vUserTaskViewModel>>(vUserTaskDtos);

                return Json(vUserTasks);
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Please set user id!"));
        }

        [HttpPost]
        [Route("user/task/add/{id}")]
        public IHttpActionResult AddUsers([FromUri]int? id, [FromBody]IEnumerable<int> userIds)
        {
            if (!id.HasValue)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "The task id value is not set!"));
            }

            _userTaskService.Save(id.Value, userIds);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "Task for users was succesfully created!"));
        }

        [HttpPut]
        [Route("user/task")]
        public IHttpActionResult SetState([FromBody]UserTaskViewModel userTask)
        {
            if (userTask == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "The user task is empty!"));
            }

            var userTaskDto = _userTaskService.GetById(userTask.UserTaskId);

            if (userTaskDto != null)
            {
                userTaskDto.StateId = userTask.StateId;

                _userTaskService.Update(userTaskDto);

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "The user task state has been changed!"));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "User task was not found"));
        }
    }
}