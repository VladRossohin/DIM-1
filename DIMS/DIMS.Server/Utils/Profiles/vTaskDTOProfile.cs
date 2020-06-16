using AutoMapper;
using DIMS.EF.DAL.Data;
using DIMS.Server.Models.Tasks;

namespace DIMS.BL.DTO
{
    internal class VTaskDTOProfile : Profile
    {
        public VTaskDTOProfile()
        {
            CreateMap<VTaskDTO, vTask>().ReverseMap();
            CreateMap<VTaskDTO, TaskViewModel>().ReverseMap();
            CreateMap<VTaskDTO, Task>().ReverseMap();
        }
    }
}
