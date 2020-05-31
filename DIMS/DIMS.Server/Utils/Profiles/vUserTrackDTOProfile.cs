using AutoMapper;
using DIMS.EF.DAL.Data;

namespace DIMS.BL.DTO
{
    internal class VUserTrackDTOProfile : Profile
    {
        public VUserTrackDTOProfile()
        {
            CreateMap<VUserTrackDTO, vUserTrack>();
        }
    }
}
