using System.Collections.Generic;

namespace DIMS.Server.Models
{
    public class UserProfilesListViewModel
    {
        public IEnumerable<UserProfileViewModel> UserProfiles { get; set; }
    }
}