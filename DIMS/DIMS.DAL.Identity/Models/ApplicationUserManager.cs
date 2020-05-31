using Microsoft.AspNet.Identity;
using System;

namespace DIMS.EF.DAL.Identity.Models
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
            //TODO: update password validation rules
            PasswordValidator = new MinimumLengthValidator(4);
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(3);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            //TODO: change provider to specificated one
            UserTokenProvider = new EmailTokenProvider<ApplicationUser>();
        }
    }
}
