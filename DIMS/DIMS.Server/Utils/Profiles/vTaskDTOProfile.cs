using AutoMapper;
using DIMS.EF.DAL.Data;

namespace DIMS.BL.DTO
{
    internal class VTaskDTOProfile : Profile
    {
        public VTaskDTOProfile()
        {
            CreateMap<VTaskDTO, vTask>();
        }
    }
}
