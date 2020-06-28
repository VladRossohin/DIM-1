using System;

namespace DIMS.Server.Models.Tasks
{
    public class VUserTaskViewModel
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public string State { get; set; }
    }
}