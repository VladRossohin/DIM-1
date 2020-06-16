using AutoMapper;
using HIMS.BL.DTO;
using HIMS.BL.Interfaces;
using HIMS.Server.Models.Directions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HIMS.Server.ControllersApi
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api")]
    public class DirectionsController : ApiController
    {
        private readonly IDirectionService _directionService;

        public DirectionsController(IDirectionService directionService)
        {
            _directionService = directionService;
        }

        
        [HttpGet]
        [Route("directions")]
        public IHttpActionResult GetDirections()
        {
            var directionDtos = _directionService.GetAll();

            var directions = Mapper.Map<IEnumerable<DirectionDTO>, IEnumerable<DirectionViewModel>>(directionDtos);

            return Json(directions);
        }
    }
}