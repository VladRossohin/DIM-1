using AutoMapper;
using DIMS.EF.DAL.Data;
using System;

namespace DIMS.BL.DTO
{
    [AutoMap(typeof(vTask))]
    public class VTaskDTO
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DeadlineDate { get; set; }


    }
}
