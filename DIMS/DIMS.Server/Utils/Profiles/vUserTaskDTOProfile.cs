using AutoMapper;
using DIMS.EF.DAL.Data;
using DIMS.Server.Models.Tasks;

namespace DIMS.BL.DTO
{
    internal class VUserTaskDTOProfile : Profile
    {
        public VUserTaskDTOProfile()
        {
            CreateMap<VUserTaskDTO, vUserTask>().ReverseMap();
            CreateMap<VUserTaskDTO, UserTaskViewModel>().ReverseMap();
            CreateMap<VUserTaskDTO, VUserTaskViewModel>().ReverseMap();
        }
    }
}
