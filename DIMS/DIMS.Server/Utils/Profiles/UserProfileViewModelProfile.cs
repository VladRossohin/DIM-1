using AutoMapper;
using DIMS.BL.DTO;

namespace DIMS.Server.Models.Users
{
    public class UserProfileViewModelProfile : Profile
    {
        public UserProfileViewModelProfile()
        {
            CreateMap<UserProfileViewModel, UserProfileDTO>();
        }
    }
}