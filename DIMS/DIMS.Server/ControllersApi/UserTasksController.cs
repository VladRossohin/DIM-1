using AutoMapper;
using HIMS.BL.DTO;
using HIMS.BL.Interfaces;
using HIMS.Server.Models.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class UserTasksController : ApiController
    {
        private readonly IUserTaskService _userTaskService;
        private readonly IvUserTaskService _vUserTaskService;

        public UserTasksController(IUserTaskService userTaskService, IvUserTaskService vUserTaskService)
        {
            _userTaskService = userTaskService;
            _vUserTaskService = vUserTaskService;
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

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "Task for users was succesfully created!"));

            }
            catch (ValidationException ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Please try again later"));
            }

        }

    }
}