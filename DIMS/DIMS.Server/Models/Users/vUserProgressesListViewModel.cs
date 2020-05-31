using System.Collections.Generic;

namespace DIMS.Server.Models.Users
{
    public class VUserProgressesListViewModel
    {
        public VUserProfileViewModel VUserProfile { get; set; }
        public IEnumerable<VUserProgressViewModel> VUserProgresses { get; set; }

        public VUserProgressesListViewModel(VUserProfileViewModel vUserProfile, IEnumerable<VUserProgressViewModel> vUserProgresses)
        {
            VUserProfile = vUserProfile;
            VUserProgresses = vUserProgresses;
        }
    }
}