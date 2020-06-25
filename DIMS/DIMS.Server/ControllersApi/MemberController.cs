using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Interfaces;
using DIMS.Server.Models;
using DIMS.Server.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace DIMS.Server.ControllersApi
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api")]
    public class MemberController : ApiController
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IVUserProfileService _vUserProfileService;
        private readonly IDirectionService _directionService;
        private readonly IMapper _mapper;

        public MemberController(IUserProfileService userProfileService,
            IVUserProfileService vUserProfileService,
            IDirectionService directionService,
            IMapper mapper)
        {
            _userProfileService = userProfileService ?? throw new ArgumentNullException(nameof(userProfileService));
            _vUserProfileService = vUserProfileService ?? throw new ArgumentNullException(nameof(vUserProfileService));
            _directionService = directionService ?? throw new ArgumentNullException(nameof(directionService));
            _mapper = mapper;
        }

        private KeyValuePair<string, IEnumerable<string>>[] GetErrors()
        {
            return ModelState
                .ToDictionary(
                k => k.Key,
                kv => kv.Value.Errors.Select(e => e.ErrorMessage)
                .Distinct()
                ).ToArray();
        }

        [HttpGet]
        [Route("profile/details/{id?}")]
        public IHttpActionResult GetDetails([FromUri] int? id)
        {
            if (!id.HasValue)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The id value was not set!"));
            }

            var vUserProfileDto = _vUserProfileService.GetById(id.Value);

            if (vUserProfileDto == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, $"The user with id = {id.Value} was not found!"));
            }

            var userProfile = _mapper.Map<VUserProfileDTO, VUserProfileViewModel>(vUserProfileDto);

            return Json(userProfile);
        }

        [HttpGet]
        [Route("profile/{id?}")]
        public IHttpActionResult GetUser([FromUri]int? id)
        {
            if (!id.HasValue)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The id value was not set!"));
            }

            var userProfileDto = _userProfileService.GetById(id.Value);

            if (userProfileDto == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, $"The user with id = {id.Value} was not found!"));
            }

            var userProfile = _mapper.Map<UserProfileDTO, UserProfileViewModel>(userProfileDto);

            return Ok(userProfile);
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create([FromBody]UserProfileViewModel userProfile)
        {
            if (ModelState.IsValid)
            {
                var userProfileDto = _mapper.Map<UserProfileViewModel, UserProfileDTO>(userProfile);
                _userProfileService.Save(userProfileDto);

                return Content<UserProfileViewModel>(HttpStatusCode.Created, userProfile);
            }

            var validationErrors = GetErrors();

            if (validationErrors != null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, validationErrors));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Oops, something went wrong! Please try again"));
        }

        [HttpPut]
        [Route("profile/edit/{id?}")]
        public IHttpActionResult Edit([FromBody] UserProfileViewModel userProfile, [FromUri] int? id)
        {
            if (ModelState.IsValid && id.HasValue)
            {
                var userProfileDto = _mapper.Map<UserProfileViewModel, UserProfileDTO>(userProfile);

                userProfileDto.UserId = id.Value;

                _userProfileService.Update(userProfileDto);

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, $"Member with id = {id.Value} has been successfully updated!"));
            }

            var validationErrors = GetErrors();

            if (validationErrors != null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, validationErrors));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Oops, something went wrong! Please try again"));
        }

        [HttpDelete]
        [Route("profile/delete/{id?}")]
        public IHttpActionResult Delete([FromUri] int? id)
        {

            if (id != null)
            {
                _userProfileService.DeleteById(id);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "The user has been succesfully deleted!"));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "The user id value is not set!"));
        }

        [HttpDelete]
        [Route("profile/deleteByEmail/{email?}")]
        public async Task<IHttpActionResult> DeleteByEmail([FromUri] string email)
        {
            if (email != null)
            {
                await _userProfileService.DeleteByEmailAsync(email);

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "The user has been succesfully deleted!"));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, $"The user email value is not set!"));
        }
    }
}