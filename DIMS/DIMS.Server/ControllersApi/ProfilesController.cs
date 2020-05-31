using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Interfaces;
using DIMS.Server.Models.Users;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DIMS.Server.ControllersApi
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api")]
    public class ProfilesController : ApiController
    {
        private readonly IVUserProfileService _vUserProfileService;
        private readonly IMapper _mapper;

        public ProfilesController(IVUserProfileService vUserProfileService, IMapper mapper)
        {
            _vUserProfileService = vUserProfileService ?? throw new ArgumentNullException(nameof(vUserProfileService));
            _mapper = mapper;
        }

        [HttpGet]
        [Route("profiles")]
        public IHttpActionResult GetProfiles()
        {
            var vUserProfileDtos = _vUserProfileService.GetAll();

            var vUserProfiles = _mapper.Map<IEnumerable<VUserProfileDTO>, List<VUserProfileViewModel>>(vUserProfileDtos);

            return Json(vUserProfiles);
        }

    }
}