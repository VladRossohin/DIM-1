using DIMS.Server.Models.Users;
using System.Collections.Generic;

namespace DIMS.Server.Models.Tasks
{
    public class TasksListViewModel
    {
        public IEnumerable<vTaskViewModel> Tasks { get; set; }
        public IEnumerable<VUserProfileViewModel> UserProfiles { get; set; }
    }
}