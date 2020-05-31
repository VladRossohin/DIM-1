using DIMS.BL.DTO;
using System.Collections.Generic;

namespace DIMS.BL.Interfaces
{
    public interface ITaskStateService : IService<TaskStateDTO>
    {
        IEnumerable<UserTaskDTO> GetUserTasks(int? id);
    }
}
