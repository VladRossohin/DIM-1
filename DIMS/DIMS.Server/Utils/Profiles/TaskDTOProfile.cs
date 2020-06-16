using AutoMapper;
using DIMS.EF.DAL.Data;
using DIMS.Server.Models.Tasks;

namespace DIMS.BL.DTO
{
    internal class TaskDTOProfile : Profile
    {
        public TaskDTOProfile()
        {
            CreateMap<TaskDTO, Task>().ReverseMap();
            CreateMap<TaskDTO, TaskViewModel>().ReverseMap();
        }
    }
}
