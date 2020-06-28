using DIMS.Server.Models.Tasks;
using System.Collections.Generic;

namespace DIMS.Server.Models.Users
{
    public class VUserProgressDetailsViewModel
    {
        public VUserProgressViewModel VUserProgress { get; set; }
        public vTaskViewModel VTask { get; set; }

        public VUserProgressDetailsViewModel(VUserProgressViewModel vUserProgress, vTaskViewModel vTask)
        {
            VTask = vTask;
            VUserProgress = vUserProgress;
        }
    }
}