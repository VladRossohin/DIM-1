using AutoMapper;
using DIMS.BL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DIMS.Server.Models.Tasks
{
    [AutoMap(typeof(TaskDTO))]
    public class TaskViewModel
    {
        public int TaskId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DeadlineDate { get; set; }

        public IEnumerable<UserTaskViewModel> UserTasks { get; set; }
    }
}