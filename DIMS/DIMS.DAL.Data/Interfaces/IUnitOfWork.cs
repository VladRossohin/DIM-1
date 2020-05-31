using DIMS.EF.DAL.Data.Interfaces;
using System;

namespace DIMS.EF.DAL.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Sample> Samples { get; }
        IRepository<UserProfile> UserProfiles { get; }
        IRepository<Direction> Directions { get; }
        IRepository<Task> Tasks { get; }
        IRepository<TaskState> TaskStates { get; }
        IRepository<TaskTrack> TaskTracks { get; }
        IUserTaskRepository UserTasks { get; }
        IViewRepository<vTask> VTasks { get; }
        IvUserProfileRepository VUserProfiles { get; }
        IViewRepository<vUserProgress> VUserProgresses { get; }
        IViewRepository<vUserTask> VUserTasks { get; }
        IViewRepository<vUserTrack> VUserTracks { get; }
        void Save();
    }
}
