using AutoMapper;
using DIMS.EF.DAL.Data;
using DIMS.Server.Models.Tasks;

namespace DIMS.BL.DTO
{
    internal class UserTaskDTOProfile : Profile
    {
        public UserTaskDTOProfile()
        {
            CreateMap<UserTaskDTO, UserTask>().ReverseMap();
            CreateMap<UserTaskDTO, UserTaskViewModel>().ReverseMap();
        }
    }
}
