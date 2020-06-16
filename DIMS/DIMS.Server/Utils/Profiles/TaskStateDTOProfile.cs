using AutoMapper;
using DIMS.EF.DAL.Data;
using DIMS.Server.Models.Tasks;

namespace DIMS.BL.DTO
{
    internal class TaskStateDTOProfile : Profile
    {
        public TaskStateDTOProfile()
        {
            CreateMap<TaskStateDTO, TaskState>().ReverseMap();
            CreateMap<TaskStateDTO, TaskStateViewModel>().ReverseMap();
        }
    }
}
