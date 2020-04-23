﻿using AutoMapper;
using HIMS.BL.DTO;
using HIMS.BL.Interfaces;
using HIMS.BL.Services;
using HIMS.Server.Models;
using HIMS.Server.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HIMS.Server.ControllersApi
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api")]
    public class MemberController : ApiController
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IvUserProfileService _vUserProfileService;
        private readonly IDirectionService _directionService;

        public MemberController(IUserProfileService userProfileService, IvUserProfileService vUserProfileService, IDirectionService directionService)
        {
            _userProfileService = userProfileService ?? throw new ArgumentNullException(nameof(userProfileService));
            _vUserProfileService = vUserProfileService ?? throw new ArgumentNullException(nameof(vUserProfileService));
            _directionService = directionService ?? throw new ArgumentNullException(nameof(directionService));
        }


        private KeyValuePair<string, IEnumerable<string>>[] GetErrors()
        {
            return ModelState
                .ToDictionary(k => k.Key, kv => kv.Value.Errors.Select(e => e.ErrorMessage).Distinct())
                .ToArray();
        }

        [HttpGet]
        [Route("profile/{id?}")]
        public IHttpActionResult GetDetails([FromUri] int? id)
        {

            if (!id.HasValue)
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The id value was not set!"));

            var vUserProfileDto = _vUserProfileService.GetItem(id.Value);

            if (vUserProfileDto == null)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, $"The user with id = {id.Value} was not found!"));

            var userProfile = Mapper.Map<vUserProfileDTO, vUserProfileViewModel>(vUserProfileDto);

            return Json(userProfile);

        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create([FromBody]UserProfileViewModel userProfile)
        {
            if(ModelState.IsValid)
            {
                var userProfileDto = Mapper.Map<UserProfileViewModel, UserProfileDTO>(userProfile);
                _userProfileService.SaveItem(userProfileDto);

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, $"The member {userProfile.Name} {userProfile.LastName} has been successfully created!"));
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
                var userProfileDto = Mapper.Map<UserProfileViewModel, UserProfileDTO>(userProfile);

                userProfileDto.UserId = id.Value;

                _userProfileService.UpdateItem(userProfileDto);

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
        public async Task<IHttpActionResult> Delete([FromUri] int? id)
        {
            try
            {
                if (id != null)
                {
                    await _userProfileService.DeleteItemAsync(id);
                }
            }
            catch
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"There's no user with id = {id.Value}"));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "The user has been succesfully deleted!"));
        }

        [HttpDelete]
        [Route("profile/deleteByEmail/{email?}")]
        public async Task<IHttpActionResult> DeleteByEmail([FromUri] string email)
        {
            try
            {
                if (email != null)
                {
                    await _userProfileService.DeleteUserProfileByEmailAsync(email);
                }
            }
            catch
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, $"There's no user with email = {email}!"));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "The user has been succesfully deleted!"));
        }

    }
}