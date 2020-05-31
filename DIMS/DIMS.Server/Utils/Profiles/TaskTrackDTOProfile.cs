using AutoMapper;
using DIMS.EF.DAL.Data;

namespace DIMS.BL.DTO
{
    internal class TaskTrackDTOProfile : Profile
    {
        public TaskTrackDTOProfile()
        {
            CreateMap<TaskTrackDTO, TaskTrack>();
        }
    }
}
