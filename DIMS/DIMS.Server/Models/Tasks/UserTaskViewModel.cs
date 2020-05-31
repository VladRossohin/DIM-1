using DIMS.EF.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIMS.Server.Models.Tasks
{
    public class UserTaskViewModel
    {
        public int UserTaskId { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public int StateId { get; set; }

        public Task Task { get; set; }
        public TaskState TaskState { get; set; }
        public IEnumerable<TaskTrack> TaskTracks { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}