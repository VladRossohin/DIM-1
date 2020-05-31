using DIMS.BL.Infrastructure;
using DIMS.BL.Interfaces;
using DIMS.BL.Models;
using DIMS.EF.DAL.Identity.Interfaces;
using DIMS.EF.DAL.Identity.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DIMS.BL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork Database { get; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserSecurityManager.FindByEmailAsync(userDto.Email).ConfigureAwait(false);

            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };

                var result = await Database.UserSecurityManager.CreateAsync(user, userDto.Password).ConfigureAwait(false);

                if (result.Errors.Any())
                {
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                }

                await Database.UserSecurityManager.AddToRoleAsync(user.Id.ToString(), userDto.Role).ConfigureAwait(false);

                await Database.SaveAsync().ConfigureAwait(false);

                return new OperationDetails(true, "The registration was done successfully!", "");
            }
            else
            {
                return new OperationDetails(false, "The user with the same name already exists!", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // finding the user
            ApplicationUser user = await Database.UserSecurityManager.FindAsync(userDto.Email, userDto.Password).ConfigureAwait(false);

            // authorize and return the ClaimsIdentity object
            if (user != null)
            {
                claim = await Database.UserSecurityManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie).ConfigureAwait(false);
            }

            return claim;
        }

        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.ApplicationRoleManager.FindByNameAsync(roleName).ConfigureAwait(false);

                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.ApplicationRoleManager.CreateAsync(role);
                }
            }

            await Create(adminDto).ConfigureAwait(false);
        }

        public async Task<OperationDetails> DeleteByEmail(string email)
        {
            ApplicationUser user = await Database.UserSecurityManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = Database.UserSecurityManager.Delete(user);

                if (result.Errors.Any())
                {
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                }

                return new OperationDetails(true, "User deletion by Email was done successfully! Email: ", nameof(email));
            }
            else
            {
                return new OperationDetails(false, "The user with such Email not found! Email: ", nameof(email));
            }
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await Database.UserSecurityManager.FindByEmailAsync(email).ConfigureAwait(false);
        }

        public async Task<ApplicationUser> FindByIdAsync(string id)
        {
            return await Database.UserSecurityManager.FindByIdAsync(id).ConfigureAwait(false);
        }

        public async Task<ApplicationUser> FindByNameAsync(string id)
        {
            return await Database.UserSecurityManager.FindByNameAsync(id).ConfigureAwait(false);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
