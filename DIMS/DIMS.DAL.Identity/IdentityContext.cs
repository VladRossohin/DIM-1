using DIMS.EF.DAL.Identity.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DIMS.EF.DAL.Identity
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(string conectionString) : base(conectionString)
        {
        }
    }
}
