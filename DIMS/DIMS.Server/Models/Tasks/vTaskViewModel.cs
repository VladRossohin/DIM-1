using AutoMapper;
using DIMS.BL.DTO;
using System;

namespace DIMS.Server.Models.Tasks
{
    [AutoMap(typeof(VTaskDTO))]
    public class vTaskViewModel
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadlineDate { get; set; }
    }
}