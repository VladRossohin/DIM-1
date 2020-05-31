using DIMS.BL.DTO;
using System.Collections.Generic;

namespace DIMS.BL.Interfaces
{
    public interface IVUserProgressService : IVService<VUserProgressDTO>
    {
        IEnumerable<VUserProgressDTO> GetByUserId(int? id);

    }
}
