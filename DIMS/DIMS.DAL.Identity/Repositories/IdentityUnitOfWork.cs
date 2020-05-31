using DIMS.EF.DAL.Identity.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace DIMS.EF.DAL.Identity.Repositories
{
    public class IdentityUnitOfWork : Interfaces.IUnitOfWork
    {
        private readonly IdentityContext _identityDbContext;

        public ApplicationRoleManager ApplicationRoleManager { get; }
        public ApplicationUserManager UserSecurityManager { get; }

        public IdentityUnitOfWork(string connectionString)
        {
            _identityDbContext = new IdentityContext(connectionString);
            ApplicationRoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_identityDbContext));
            UserSecurityManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_identityDbContext));
        }

        public async System.Threading.Tasks.Task SaveAsync()
        {
            await _identityDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //release managed resources
                    _identityDbContext.Dispose();
                    ApplicationRoleManager.Dispose();
                    UserSecurityManager.Dispose();
                }
                //relesae unmanaged resources
                disposed = true;
            }
        }
    }
}
