using AutoMapper;
using DIMS.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIMS.Server.Models.Tasks
{
    [AutoMap(typeof(TaskStateDTO))]
    public class TaskStateViewModel
    {
        public int StateId { get; set; }
        public string StateName { get; set; }

        public IEnumerable<UserTaskDTO> UserTasks { get; set; }
    }
}