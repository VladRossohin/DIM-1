using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Interfaces;
using DIMS.Server.Models.Tasks;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;


namespace DIMS.Server.Controllers.Menthor
{
    [Authorize]
    public class TaskTrackController : Controller
    {
        private readonly ITaskTrackService _taskTrackService;
        private readonly IVTaskTrackService _vTaskTrackService;
        private readonly IVUserTrackService _vUserTrackService;
        private readonly IMapper _mapper;

        public TaskTrackController(ITaskTrackService taskTrackService, 
            IVTaskTrackService vTaskTrackService, 
            IVUserTrackService vUserTrackService, 
            IMapper mapper)
        {
            _taskTrackService = taskTrackService;
            _vTaskTrackService = vTaskTrackService;
            _vUserTrackService = vUserTrackService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("track/{userId}")]
        public ActionResult Index(int userId)
        {
            var vUserTrackDTOs = _vUserTrackService.GetTracksForUser(userId);

            var taskTrackViewModels = _mapper.Map<IEnumerable<VUserTrackDTO>, IEnumerable<TaskTrackViewModel>>(vUserTrackDTOs);

            return View(taskTrackViewModels);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var taskTrackDto = _vUserTrackService.GetById(id);

            if (taskTrackDto == null)
            {
                return HttpNotFound();
            }

            var taskTrackViewModel = _mapper.Map<VUserTrackDTO, TaskTrackViewModel>(taskTrackDto);

            return View(taskTrackViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskTrackViewModel taskTrackViewModel, int id)
        {
            var vTaskTrackDto = _vTaskTrackService.GetById(id);
            var vUserTrackDto = _vUserTrackService.GetById(id);
            vTaskTrackDto.TrackDate = taskTrackViewModel.TrackDate;
            vTaskTrackDto.TrackNote = taskTrackViewModel.TrackNote;

            try
            {
                _vTaskTrackService.Update(vTaskTrackDto);
                return RedirectToAction("Index", new { userId = vUserTrackDto.UserId });
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return PartialView(taskTrackViewModel);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {

            var vUserTrackDto = _vUserTrackService.GetById(id);

            if (vUserTrackDto == null)
            {
                return HttpNotFound();
            }

            var taskTrackViewModel = _mapper.Map<VUserTrackDTO, TaskTrackViewModel>(vUserTrackDto);

            return View(taskTrackViewModel);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {

            var vUserTrackDto = _vUserTrackService.GetById(id);

            if (vUserTrackDto == null)
            {
                return HttpNotFound();
            }

            var taskTrackViewModel = _mapper.Map<VUserTrackDTO, TaskTrackViewModel>(vUserTrackDto);

            return View(taskTrackViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            var vUserTrackDto = _vUserTrackService.GetById(id.Value);

            try
            {
                _taskTrackService.DeleteById(id);
            }
            catch (RetryLimitExceededException)
            {
                return RedirectToAction("Delete", new { id, saveChangesError = true });
            }

            return RedirectToAction("Index", new { userId = vUserTrackDto.UserId });
        }
    }
}