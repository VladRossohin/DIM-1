using AutoMapper;
using DIMS.EF.DAL.Data;

namespace DIMS.BL.DTO
{
    internal class VTaskTrackDTOProfile : Profile
    {
        public VTaskTrackDTOProfile()
        {
            CreateMap<VTaskTrackDTO, TaskTrack>().ReverseMap();
        }
    }
}
