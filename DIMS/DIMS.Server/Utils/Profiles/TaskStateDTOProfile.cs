using AutoMapper;
using DIMS.EF.DAL.Data;

namespace DIMS.BL.DTO
{
    internal class TaskStateDTOProfile : Profile
    {
        public TaskStateDTOProfile()
        {
            CreateMap<TaskStateDTO, TaskState>();
        }
    }
}
