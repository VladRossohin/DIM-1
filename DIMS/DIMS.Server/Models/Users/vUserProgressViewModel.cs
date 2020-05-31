using AutoMapper;
using DIMS.BL.DTO;
using System;

namespace DIMS.Server.Models.Users
{
    [AutoMap(typeof(VUserProgressDTO))]
    public class VUserProgressViewModel
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public int TaskTrackId { get; set; }
        public string UserName { get; set; }
        public string TaskName { get; set; }
        public string TrackNote { get; set; }
        public DateTime? TrackDate { get; set; }
    }
}