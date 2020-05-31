using DIMS.EF.DAL.Identity.Models;
using System;
using System.Threading.Tasks;

namespace DIMS.EF.DAL.Identity.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationRoleManager ApplicationRoleManager { get; }
        ApplicationUserManager UserSecurityManager { get; }
        Task SaveAsync();
    }
}
