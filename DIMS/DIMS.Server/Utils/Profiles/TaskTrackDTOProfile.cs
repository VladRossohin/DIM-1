using AutoMapper;
using DIMS.EF.DAL.Data;
using DIMS.Server.Models.Tasks;

namespace DIMS.BL.DTO
{
    internal class TaskTrackDTOProfile : Profile
    {
        public TaskTrackDTOProfile()
        {
            CreateMap<TaskTrackDTO, TaskTrack>().ReverseMap();
            CreateMap<TaskTrackDTO, TaskTrackViewModel>().ReverseMap();
        }
    }
}
