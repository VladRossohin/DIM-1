using DIMS.BL.DTO;
using System.Collections.Generic;

namespace DIMS.BL.Interfaces
{
    public interface ITaskService : IService<TaskDTO>, IVService<TaskDTO>
    {
        IEnumerable<UserTaskDTO> GetUserTasks(int? id);
    }
}
