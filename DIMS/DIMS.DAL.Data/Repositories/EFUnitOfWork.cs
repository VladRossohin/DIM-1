using DIMS.EF.DAL.Data.Interfaces;
using System;

namespace DIMS.EF.DAL.Data.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly DIMSDBContext _dimsDbContext;

        private SampleRepository _sampleRepository;
        private UserProfileRepository _userProfileRepository;
        private UserTaskRepository _userTaskRepository;
        private TaskRepository _taskRepository;
        private TaskStateRepository _taskStateRepository;
        private TaskTrackRepository _taskTrackRepository;
        private DirectionRepository _directionRepository;
        private VTaskRepository _vTaskRepository;
        private VUserProfileRepository _vUserProfileRepository;
        private VUserProgressRepository _vUserProgressRepository;
        private VUserTaskRepository _vUserTaskRepository;
        private VUserTrackRepository _vUserTrackRepository;

        public IRepository<Sample> Samples => _sampleRepository ?? (_sampleRepository = new SampleRepository(_dimsDbContext));

        public IRepository<UserProfile> UserProfiles => _userProfileRepository ?? (_userProfileRepository = new UserProfileRepository(_dimsDbContext));
        
        public IUserTaskRepository UserTasks => _userTaskRepository ?? (_userTaskRepository = new UserTaskRepository(_dimsDbContext));
        
        public IRepository<Direction> Directions => _directionRepository ?? (_directionRepository= new DirectionRepository(_dimsDbContext));

        public IRepository<Task> Tasks => _taskRepository ?? (_taskRepository = new TaskRepository(_dimsDbContext));

        public IRepository<TaskState> TaskStates => _taskStateRepository ?? (_taskStateRepository = new TaskStateRepository(_dimsDbContext));

        public IRepository<TaskTrack> TaskTracks => _taskTrackRepository ?? (_taskTrackRepository = new TaskTrackRepository(_dimsDbContext));

        public IViewRepository<vTask> VTasks => _vTaskRepository ?? (_vTaskRepository = new VTaskRepository(_dimsDbContext));

        public IViewRepository<vUserProgress> VUserProgresses => _vUserProgressRepository ?? (_vUserProgressRepository = new VUserProgressRepository(_dimsDbContext));

        public IViewRepository<vUserTask> VUserTasks => _vUserTaskRepository ?? (_vUserTaskRepository = new VUserTaskRepository(_dimsDbContext));

        public IViewRepository<vUserTrack> VUserTracks => _vUserTrackRepository ?? (_vUserTrackRepository = new VUserTrackRepository(_dimsDbContext));

        public IvUserProfileRepository VUserProfiles => _vUserProfileRepository ?? (_vUserProfileRepository = new VUserProfileRepository(_dimsDbContext));

        public EFUnitOfWork(string connectionString)
        {
            _dimsDbContext = new DIMSDBContext(connectionString);
        }
        public void Save()
        {
            _dimsDbContext.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //Release managed resources
                    _dimsDbContext.Dispose();
                }
                //release unmanaged resources
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
