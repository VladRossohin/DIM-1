using DIMS.BL.DTO;
using System.Threading.Tasks;

namespace DIMS.BL.Interfaces
{
    public interface IVUserProfileService : IVService<VUserProfileDTO>
    {
        Task<VUserProfileDTO> GetByEmailAsync(string email);

    }
}
