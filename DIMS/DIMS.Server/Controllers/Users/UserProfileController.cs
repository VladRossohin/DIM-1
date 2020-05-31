using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Interfaces;
using DIMS.Server.Models;
using DIMS.Server.Models.Directions;
using DIMS.Server.Models.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DIMS.Server.Controllers
{
    [Authorize]
    [RoutePrefix("users")]
    public class UserProfileController : Controller
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IVUserProfileService _vUserProfileService;
        private readonly IDirectionService _directionService;
        private readonly IVUserProgressService _vUserProgressService;
        private readonly IMapper _mapper;

        public UserProfileController(IUserProfileService userProfileService,
            IVUserProfileService vUserProfileService,
            IDirectionService directionService,
            IVUserProgressService vUserProgressService,
            IMapper mapper)
        {
            _userProfileService = userProfileService;
            _vUserProfileService = vUserProfileService;
            _directionService = directionService;
            _vUserProgressService = vUserProgressService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("profiles")]
        public ActionResult Index()
        {
            var userProfileDtos = _vUserProfileService.GetAll();

            var userProfileListViewModel = _mapper.Map<IEnumerable<VUserProfileDTO>, IEnumerable<VUserProfileViewModel>>(userProfileDtos);

            return View(userProfileListViewModel);
        }

        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            var directions = GetDirections();

            ViewBag.DirectionId = directions;

            return PartialView();
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserProfileViewModel userProfileViewModel)
        {
            try
            {
                var userProfileDTO = _mapper.Map<UserProfileViewModel, UserProfileDTO>(userProfileViewModel);

                if (ModelState.IsValid)
                {
                    _userProfileService.Save(userProfileDTO);

                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.ValidationAttribute.ToString(), ex.Message);
            }

            ViewBag.DirectionId = GetDirections();

            return PartialView(userProfileViewModel);
        }

        [HttpGet]
        [Route("edit/{id?}")]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userProfileDto = _userProfileService.GetById(id.Value);

            if (userProfileDto == null)
            {
                return HttpNotFound();
            }

            var UserProfileViewModel = _mapper.Map<UserProfileDTO, UserProfileViewModel>(userProfileDto);

            ViewBag.DirectionId = GetDirections();

            return PartialView(UserProfileViewModel);
        }

        [HttpPost, ActionName("Edit")]
        [Route("edit/{id?}")]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(UserProfileViewModel userProfileViewModel, int? id)
        {
            if (userProfileViewModel == null || !id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userProfileDto = _userProfileService.GetById(id.Value);

            try
            {
                _mapper.Map(userProfileViewModel, userProfileDto);

                userProfileDto.UserId = id.Value;

                _userProfileService.Update(userProfileDto);

                return RedirectToAction("Index");
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            var UserProfileViewModel = _mapper.Map<UserProfileDTO, UserProfileViewModel>(userProfileDto);

            return PartialView(UserProfileViewModel);
        }

        [HttpGet]
        [Route("profile/{id?}")]
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vuserProfileDto = _vUserProfileService.GetById(id.Value);

            if (vuserProfileDto == null)
            {
                return HttpNotFound();
            }

            var vuserProfile = _mapper.Map<VUserProfileDTO, VUserProfileViewModel>(vuserProfileDto);

            return PartialView(vuserProfile);
        }

        [HttpGet]
        [Route("profile/progress/{id?}")]
        public ActionResult Progress(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userProgressDto = _vUserProgressService.GetByUserId(id.Value);

            var user = _vUserProfileService.GetById(id.Value);

            var vUserProgressesListViewModel = new VUserProgressesListViewModel(
                _mapper.Map<VUserProfileDTO, VUserProfileViewModel>(user),
                _mapper.Map<IEnumerable<VUserProgressDTO>, IEnumerable<VUserProgressViewModel>>(userProgressDto)
                );

            return PartialView(vUserProgressesListViewModel);
        }

        public ActionResult DeleteById(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vuserProfileDto = _vUserProfileService.GetById(id.Value);

            if (vuserProfileDto == null)
            {
                return HttpNotFound();
            }

            var vuserProfile = _mapper.Map<VUserProfileDTO, VUserProfileViewModel>(vuserProfileDto);

            return View(vuserProfile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteById(int id)
        {
            try
            {
                _userProfileService.DeleteById(id);
            }
            catch (RetryLimitExceededException)
            {
                return RedirectToAction("DeleteById", new { id, saveChangesError = true });
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("delete/{email?}")]
        public async Task<ActionResult> DeleteByEmail(string email)
        {
            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userProfile = await _vUserProfileService.GetByEmailAsync(email);

            if (userProfile == null)
            {
                return HttpNotFound();
            }

            var vuserProfileDto = _mapper.Map<VUserProfileDTO, VUserProfileViewModel>(userProfile);

            return PartialView(vuserProfileDto);
        }

        [HttpDelete]
        [Route("delete/{email?}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteByEmail(string email, int id)
        {
            try
            {
                var operationDetails = await _userProfileService.DeleteByEmailAsync(email);
            }
            catch (RetryLimitExceededException)
            {
                return RedirectToAction("DeleteByEmail", new { email, saveChangesError = true });
            }

            return RedirectToAction("Index");
        }

        [NonAction]
        private List<SelectListItem> GetDirections()
        {
            var direstionDtos = _directionService.GetAll();

            var directions = _mapper.Map<IEnumerable<DirectionDTO>, List<DirectionViewModel>>(direstionDtos);

            var selectedItems = new List<SelectListItem>();

            foreach (var direction in directions)
            {
                selectedItems.Add(new SelectListItem { Text = direction.Name, Value = direction.DirectionId.ToString() });
            }

            return selectedItems;
        }
    }
}