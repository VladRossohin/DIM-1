using DIMS.BL.DTO;
using System.Collections.Generic;

namespace DIMS.BL.Interfaces
{
    public interface IUserTaskService : IService<UserTaskDTO>
    {
        TaskDTO GetTask(int id);
        TaskStateDTO GetTaskState(int id);
        IEnumerable<TaskTrackDTO> GetTaskTracks(int id);
        IEnumerable<UserTaskDTO> GetByUserId(int id);
        UserProfileDTO GetUserProfile(int id);
        IEnumerable<UserTaskDTO> GetAllUserTasksByTaskId(int id);
        void DeleteItemByTaskIdAndUserId(int taskId, int userId);
    }
}
