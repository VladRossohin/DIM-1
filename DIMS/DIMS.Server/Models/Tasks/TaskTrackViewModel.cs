using AutoMapper;
using DIMS.BL.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace DIMS.Server.Models.Tasks
{
    [AutoMap(typeof(VUserTrackDTO))]
    public class TaskTrackViewModel
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public int TaskTrackId { get; set; }

        public string TaskName { get; set; }
        public string TrackNote { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TrackDate { get; set; }
    }
}