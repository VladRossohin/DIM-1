using DIMS.BL.DTO;
using System;

namespace DIMS.Server.Models.Tasks
{
    public class TaskTrackViewModel
    {
        public int TaskTrackId { get; set; }
        public int UserTaskId { get; set; }
        public DateTime? TrackDate { get; set; }
        public string TrackNote { get; set; }
        public UserTaskDTO UserTask { get; set; }
    }
}