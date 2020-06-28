using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIMS.Server.Models.Tasks
{
    public class TaskTrackListViewModel
    {
        public IEnumerable<VUserTrackViewModel> VUserTrackViewModels { get; set; }
        public int UserId { get; set; }
    }
}