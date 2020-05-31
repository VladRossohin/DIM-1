using AutoMapper;
using DIMS.BL.DTO;

namespace DIMS.Server.Models.Users
{
    public class VUserProgressViewModelProfile : Profile
    {
        public VUserProgressViewModelProfile()
        {
            CreateMap<VUserProgressViewModel, VUserProgressDTO>();
        }
    }
}