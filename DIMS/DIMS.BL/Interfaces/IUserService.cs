using DIMS.BL.Infrastructure;
using DIMS.BL.Models;
using DIMS.EF.DAL.Identity.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DIMS.BL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        Task<OperationDetails> DeleteByEmail(string email);

        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<ApplicationUser> FindByNameAsync(string email);
        Task<ApplicationUser> FindByIdAsync(string email);

    }
}
