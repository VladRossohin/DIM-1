using AutoMapper;
using DIMS.EF.DAL.Data;
using System;

namespace DIMS.BL.DTO
{
    [AutoMap(typeof(vUserTask))]
    public class VUserTaskDTO
    {
        public int UserTaskId { get; set; }
        public int UserId { get; set; }
        public int StateId { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public string State { get; set; }

    }
}
