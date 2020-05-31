using AutoMapper;
using DIMS.EF.DAL.Data;

namespace DIMS.BL.DTO
{
    internal class VUserTaskDTOProfile : Profile
    {
        public VUserTaskDTOProfile()
        {
            CreateMap<VUserTaskDTO, vUserTask>();
        }
    }
}
