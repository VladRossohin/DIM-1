using System.Threading.Tasks;

namespace DIMS.EF.DAL.Data.Interfaces
{
    public interface IvUserProfileRepository : IViewRepository<vUserProfile>
    {
        Task<vUserProfile> GetByEmailAsync(string email);
    }
}
