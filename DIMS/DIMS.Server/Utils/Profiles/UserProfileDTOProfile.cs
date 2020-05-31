using AutoMapper;
using DIMS.EF.DAL.Data;

namespace DIMS.BL.DTO
{
    internal class UserProfileDTOProfile : Profile
    {
        public UserProfileDTOProfile()
        {
            CreateMap<UserProfileDTO, UserProfile>();
            CreateMap<UserProfile, UserProfileDTO>();
        }
    }
}
