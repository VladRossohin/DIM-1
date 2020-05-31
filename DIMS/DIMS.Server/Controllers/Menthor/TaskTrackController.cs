using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Interfaces;
using DIMS.Server.Models.Tasks;
using System.Collections.Generic;
using System.Web.Mvc;


namespace DIMS.Server.Controllers.Menthor
{
    [Authorize]
    [RoutePrefix("task")]
    public class TaskTrackController : Controller
    {
        private readonly ITaskTrackService _taskTrackService;
        private readonly IMapper _mapper;

        public TaskTrackController(ITaskTrackService taskTrackService, IMapper mapper)
        {
            _taskTrackService = taskTrackService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("track")]
        public ActionResult Index(int userId)
        {
            IEnumerable<TaskTrackDTO> taskTrackDTOs = _taskTrackService.GetTracksForUser(userId);

            var taskTrackListViewModel = new TaskTrackListViewModel
            {
                TaskTrackViewModels = _mapper.Map<IEnumerable<TaskTrackDTO>, IEnumerable<TaskTrackViewModel>>(taskTrackDTOs),
                UserId = userId
            };

            return View(taskTrackListViewModel);
        }
    }
}