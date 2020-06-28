using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Interfaces;
using DIMS.Server.Models.Tasks;
using DIMS.Server.Models.Users;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace DIMS.Server.Controllers.Users
{
    [RoutePrefix("progress")]
    public class UserProgressController : Controller
    {
        private readonly IVUserProgressService _userProgressService;
        private readonly IVUserProfileService _userProfileService;
        private readonly IVTaskService _taskService;
        private readonly IMapper _mapper;

        public UserProgressController(IVUserProgressService userProgressService, 
            IVUserProfileService userProfileService, 
            IMapper mapper,
            IVTaskService taskService)
        {
            _userProgressService = userProgressService;
            _userProfileService = userProfileService;
            _taskService = taskService;
            _mapper = mapper;
        }
        [Route("user/{id}")]
        public ActionResult Index(int id)
        {
            var userProfileDTO = _userProfileService.GetById(id);

            var userProfileViewModel = _mapper.Map<VUserProfileDTO, VUserProfileViewModel>(userProfileDTO);

            var userProgressDTO = _userProgressService.GetByUserId(id);

            var userProgressViewModel = _mapper.Map<IEnumerable<VUserProgressDTO>, IEnumerable<VUserProgressViewModel>>(userProgressDTO);

            var userProgresses = new VUserProgressesListViewModel(userProfileViewModel, userProgressViewModel);

            return View(userProgresses);
        }

        [Route("detail/{id}")]
        public ActionResult Details(int id)
        {
            var userProgressDTO = _userProgressService.GetById(id);

            var userProgressViewModel = _mapper.Map<VUserProgressDTO, VUserProgressViewModel>(userProgressDTO);

            var taskDTO = _taskService.GetById(userProgressDTO.TaskId);

            var taskViewModel = _mapper.Map<VTaskDTO, vTaskViewModel>(taskDTO);

            var userProgressDetails = new VUserProgressDetailsViewModel(userProgressViewModel, taskViewModel);

            return View(userProgressDetails);
        }
    }
}