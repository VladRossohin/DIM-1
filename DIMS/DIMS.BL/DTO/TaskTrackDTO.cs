using AutoMapper;
using DIMS.EF.DAL.Data;
using System;

namespace DIMS.BL.DTO
{
    [AutoMap(typeof(TaskTrack))]
    public class TaskTrackDTO
    {
        public int TaskTrackId { get; set; }
        public int UserTaskId { get; set; }
        public DateTime TrackDate { get; set; }
        public string TrackNote { get; set; }
        public virtual UserTaskDTO UserTask { get; set; }

    }
}
