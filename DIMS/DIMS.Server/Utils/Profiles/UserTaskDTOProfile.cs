using AutoMapper;
using DIMS.EF.DAL.Data;

namespace DIMS.BL.DTO
{
    internal class UserTaskDTOProfile : Profile
    {
        public UserTaskDTOProfile()
        {
            CreateMap<UserTaskDTO, UserTask>();
        }
    }
}
