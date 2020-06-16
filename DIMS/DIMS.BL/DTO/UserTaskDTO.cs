using AutoMapper;
using DIMS.EF.DAL.Data;
using System.Collections.Generic;

namespace DIMS.BL.DTO
{
    [AutoMap(typeof(UserTask))]
    public class UserTaskDTO
    {

        public UserTaskDTO()
        {
            TaskTracks = new HashSet<TaskTrackDTO>();
        }
        
        public int UserTaskId { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public int StateId { get; set; }

        public virtual TaskDTO Task { get; set; }
        public virtual TaskStateDTO TaskState { get; set; }
        public virtual ICollection<TaskTrackDTO> TaskTracks { get; set; }
        public virtual UserProfileDTO UserProfile { get; set; }
    }
}
