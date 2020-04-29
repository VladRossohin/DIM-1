using HIMS.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Interfaces
{
    public interface IUserTaskService : IService<UserTaskDTO>
    {
        TaskDTO GetTask(int? id);
        TaskStateDTO GetTaskState(int? id);
        IEnumerable<TaskTrackDTO> GetTaskTracks(int? id);
        IEnumerable<UserTaskDTO> GetByUserId(int? id);
        UserProfileDTO GetUserProfile(int? id);

        UserTaskDTO GetByTaskIdAndUserId(int? taskId, int? userId);
    }
}
