using DIMS.Server.Models.Directions;
using System.Collections.Generic;

namespace DIMS.Server.Models.Users
{
    public class UserProfilePageViewModel
    {
        public UserProfileViewModel UserProfileViewModel { get; set; }
        public UserProfilesListViewModel UserProfilesListViewModel { get; set; }
        public VUserProfileViewModel vUserProfileViewModel { get; set; }
        public VUserProfilesListViewModel vUserProfilesListViewModel { get; set; }
        public VUserProgressesListViewModel vUserProgressesListViewModel { get; set; }

        public IEnumerable<DirectionViewModel> DirectionViewModels { get; set; }
    }
}