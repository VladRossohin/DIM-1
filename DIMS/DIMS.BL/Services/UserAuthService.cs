using DIMS.BL.Interfaces;
using DIMS.BL.Models;
using DIMS.EF.DAL.Identity.Interfaces;
using System.Threading.Tasks;

namespace DIMS.BL.Services
{
    public class UserAuthService : IAuthService<UserDTO>
    {
        private IUnitOfWork Database { get; }

        public UserAuthService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<string> GenerateTokenAsync(UserDTO userDto)
        {
            var user = await Database.UserSecurityManager.FindByEmailAsync(userDto.Email).ConfigureAwait(false);

            var token = await Database.UserSecurityManager.GenerateEmailConfirmationTokenAsync(user.Id).ConfigureAwait(false);

            return token;
        }
    }
}
