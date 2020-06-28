using AutoMapper;
using DIMS.EF.DAL.Data;

namespace DIMS.BL.DTO
{
    internal class VUserProgressDTOProfile : Profile
    {
        public VUserProgressDTOProfile()
        {
            CreateMap<VUserProgressDTO, vUserProgress>().ReverseMap();
        }
    }
}
