using AutoMapper;
using DIMS.BL.DTO;

namespace DIMS.Server.Models.Directions
{
    public class DirectionViewModelProfile : Profile
    {
        public DirectionViewModelProfile()
        {
            CreateMap<DirectionViewModel, DirectionDTO>();
        }
    }
}