using DIMS.BL.DTO;
using System.Collections.Generic;

namespace DIMS.BL.Interfaces
{
    public interface IVUserTrackService : IVService<VUserTrackDTO>
    {
        IEnumerable<VUserTrackDTO> GetTracksForUser(int userId);
    }
}
