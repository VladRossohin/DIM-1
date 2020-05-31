using AutoMapper;
using DIMS.EF.DAL.Data;
using System.Collections.Generic;

namespace DIMS.BL.DTO
{
    internal class VUserProfileDTOProfile : Profile
    {
        public VUserProfileDTOProfile()
        {
            CreateMap<VUserProfileDTO, vUserProfile>();
            CreateMap<vUserProfile, VUserProfileDTO>();
        }
    }
}
