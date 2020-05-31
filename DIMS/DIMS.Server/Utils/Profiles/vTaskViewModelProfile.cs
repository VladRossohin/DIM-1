using AutoMapper;
using DIMS.BL.DTO;

namespace DIMS.Server.Models.Tasks
{
    public class vTaskViewModelProfile : Profile
    {
        public vTaskViewModelProfile()
        {
            CreateMap<vTaskViewModel, VTaskDTO>();
        }
    }
}