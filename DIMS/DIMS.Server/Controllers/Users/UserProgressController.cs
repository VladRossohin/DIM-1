using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Interfaces;
using DIMS.Server.Models.Users;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DIMS.Server.Controllers.Users
{
    public class UserProgressController : Controller
    {
        private readonly IVUserProgressService _userProgressService;
        private readonly IVUserProfileService _userProfileService;
        private readonly IMapper _mapper;

        public UserProgressController(IVUserProgressService userProgressService, IVUserProfileService userProfileService, IMapper mapper)
        {
            _userProgressService = userProgressService;
            _userProfileService = userProfileService;
            _mapper = mapper;
        }

        public ActionResult Index(int? id)
        {
            if (!id.HasValue)
            {
                // TODO: create my own badRequest method
                return HttpNotFound();
            }

            var _userProfile = _userProfileService.GetById(id.Value);

            var userProfile = _mapper.Map<VUserProfileDTO, VUserProfileViewModel>(_userProfile);

            var _userProgress = _userProgressService.GetByUserId(id.Value);

            var userProgress = _mapper.Map<IEnumerable<VUserProgressDTO>, IEnumerable<VUserProgressViewModel>>(_userProgress);

            var userProgresses = new VUserProgressesListViewModel(userProfile, userProgress);

            return View(userProgresses);
        }
    }
}