using AutoMapper;
using DIMS.EF.DAL.Data;
using System.Collections.Generic;

namespace DIMS.BL.DTO
{
    [AutoMap(typeof(TaskState))]
    public class TaskStateDTO
    {
        public TaskStateDTO()
        {
            UserTasks = new HashSet<UserTaskDTO>();
        }

        public int StateId { get; set; }
        public string StateName { get; set; }
        public virtual ICollection<UserTaskDTO> UserTasks { get; set; }
    }
}
