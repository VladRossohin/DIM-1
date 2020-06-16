using System;

namespace DIMS.BL.DTO
{
    public class VTaskTrackDTO
    {
        public int TaskTrackId { get; set; }
        public int UserTaskId { get; set; }
        public DateTime TrackDate { get; set; }
        public string TrackNote { get; set; }

    }
}
