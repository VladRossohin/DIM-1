using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Interfaces;
using DIMS.Server.Models.Directions;
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

        private readonly IMapper _mapper;

        public DirectionsController(IDirectionService directionService, IMapper mapper)
        {
            _directionService = directionService;
            _mapper = mapper;
        }

        
        [HttpGet]
        [Route("directions")]
        public IHttpActionResult GetDirections()
        {
            var directionDtos = _directionService.GetAll();

            var directions = _mapper.Map<IEnumerable<DirectionDTO>, IEnumerable<DirectionViewModel>>(directionDtos);

            return Json(directions);
        }
    }
}