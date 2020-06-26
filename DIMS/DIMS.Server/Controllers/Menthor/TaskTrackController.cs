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
    [Authorize(Roles = "user" )]
    [RoutePrefix("track")]
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
        [Route("{id}")]
        public ActionResult Index(int id)
        {
            var vUserTrackDTOs = _vUserTrackService.GetTracksForUser(id);

            var VUserTrackViewModels = _mapper.Map<IEnumerable<VUserTrackDTO>, IEnumerable<VUserTrackViewModel>>(vUserTrackDTOs);

            return View(VUserTrackViewModels);
        }

        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int id)
        {
            var taskTrackDto = _vUserTrackService.GetById(id);

            if (taskTrackDto == null)
            {
                return HttpNotFound();
            }

            var VUserTrackViewModel = _mapper.Map<VUserTrackDTO, VUserTrackViewModel>(taskTrackDto);

            return View(VUserTrackViewModel);
        }

        [HttpPost]
        [Route("edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VUserTrackViewModel VUserTrackViewModel, int id)
        {
            var vTaskTrackDto = _vTaskTrackService.GetById(id);
            var vUserTrackDto = _vUserTrackService.GetById(id);
            vTaskTrackDto.TrackDate = VUserTrackViewModel.TrackDate;
            vTaskTrackDto.TrackNote = VUserTrackViewModel.TrackNote;

            try
            {
                _vTaskTrackService.Update(vTaskTrackDto);
                return RedirectToAction("Index", new { id = vUserTrackDto.UserId });
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return PartialView(VUserTrackViewModel);
        }

        [HttpGet]
        [Route("details/{id}")]
        public ActionResult Details(int id)
        {
            var vUserTrackDto = _vUserTrackService.GetById(id);

            if (vUserTrackDto == null)
            {
                return HttpNotFound();
            }

            var VUserTrackViewModel = _mapper.Map<VUserTrackDTO, VUserTrackViewModel>(vUserTrackDto);

            return View(VUserTrackViewModel);
        }

        [HttpGet]
        [Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            var vUserTrackDto = _vUserTrackService.GetById(id);

            if (vUserTrackDto == null)
            {
                return HttpNotFound();
            }

            var VUserTrackViewModel = _mapper.Map<VUserTrackDTO, VUserTrackViewModel>(vUserTrackDto);

            return View(VUserTrackViewModel);
        }

        [HttpPost]
        [Route("delete/{id?}")]
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

            return RedirectToAction("Index", new { id = vUserTrackDto.UserId });
        }
    }
}