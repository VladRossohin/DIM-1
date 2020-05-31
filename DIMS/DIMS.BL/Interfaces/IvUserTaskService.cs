using DIMS.BL.DTO;
using System.Collections.Generic;

namespace DIMS.BL.Interfaces
{
    public interface IVUserTaskService : IVService<VUserTaskDTO>
    {
        void Save(VUserTaskDTO vUserTaskDTO);
        void Update(VUserTaskDTO vUserTaskDTO);
        IEnumerable<VUserTaskDTO> GetByUserId(int id);
    }
}
