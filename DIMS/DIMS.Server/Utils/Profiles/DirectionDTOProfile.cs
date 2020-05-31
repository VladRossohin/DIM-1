using AutoMapper;
using DIMS.EF.DAL.Data;

namespace DIMS.BL.DTO
{
    internal class DirectionDTOProfile : Profile
    {
        public DirectionDTOProfile()
        {
            CreateMap<DirectionDTO, Direction>();
            CreateMap<Direction, DirectionDTO>();
        }
    }
}
