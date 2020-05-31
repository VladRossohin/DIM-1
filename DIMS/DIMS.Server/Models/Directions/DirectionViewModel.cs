using AutoMapper;
using DIMS.BL.DTO;

namespace DIMS.Server.Models.Directions
{
    [AutoMap(typeof(DirectionDTO))]
    public class DirectionViewModel
    {
        public int DirectionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}