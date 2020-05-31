using AutoMapper;
using DIMS.BL.DTO;

namespace DIMS.Server.Models.Users
{
    public class VUserProfileViewModelProfile : Profile
    {
        public VUserProfileViewModelProfile()
        {
            CreateMap<VUserProfileViewModel, VUserProfileDTO>();
        }
    }
}