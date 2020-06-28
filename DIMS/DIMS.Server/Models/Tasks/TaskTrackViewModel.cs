using AutoMapper;
using DIMS.BL.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace DIMS.Server.Models.Tasks
{
    [AutoMap(typeof(TaskTrackDTO))]
    public class TaskTrackViewModel
    {
        public int TaskTrackId { get; set; }
        public int UserTaskId { get; set; }
        public DateTime TrackDate { get; set; }
        public string TrackNote { get; set; }
    }
}