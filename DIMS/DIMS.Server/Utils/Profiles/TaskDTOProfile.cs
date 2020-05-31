using AutoMapper;
using DIMS.EF.DAL.Data;

namespace DIMS.BL.DTO
{
    internal class TaskDTOProfile : Profile
    {
        public TaskDTOProfile()
        {
            CreateMap<TaskDTO, Task>();
        }
    }
}
