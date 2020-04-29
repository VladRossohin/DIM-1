using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.EF.DAL.Data.Interfaces;

namespace HIMS.EF.DAL.Data.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly DIMSDBContext _dimsDbContext;
        private readonly SampleRepository _sampleRepository;
        private readonly UserProfileRepository _userProfileRepository;
        private readonly UserTaskRepository _userTaskRepository;
        private readonly TaskRepository _taskRepository;
        private readonly TaskStateRepository _taskStateRepository;
        private readonly TaskTrackRepository _taskTrackRepository;
        private readonly DirectionRepository _directionRepository;
        private readonly vTaskRepository _vTaskRepository;
        private readonly vUserProfileRepository _vUserProfileRepository;
        private readonly vUserProgressRepository _vUserProgressRepository;
        private readonly vUserTaskRepository _vUserTaskRepository;
        private readonly vUserTrackRepository _vUserTrackRepository;

        public IRepository<Sample> Samples => _sampleRepository ?? new SampleRepository(_dimsDbContext);
       
        public IRepository<UserProfile> UserProfiles => _userProfileRepository ?? new UserProfileRepository(_dimsDbContext);
        
        public IUserTaskRepository UserTasks => _userTaskRepository ?? new UserTaskRepository(_dimsDbContext);
        
        public IRepository<Direction> Directions => _directionRepository ?? new DirectionRepository(_dimsDbContext);
        
        public IRepository<Task> Tasks => _taskRepository ?? new TaskRepository(_dimsDbContext);
        
        public IRepository<TaskState> TaskStates => _taskStateRepository ?? new TaskStateRepository(_dimsDbContext);
        
        public IRepository<TaskTrack> TaskTracks => _taskTrackRepository ?? new TaskTrackRepository(_dimsDbContext);

        public IViewRepository<vTask> vTasks => _vTaskRepository ?? new vTaskRepository(_dimsDbContext);

        public IViewRepository<vUserProgress> vUserProgresses => _vUserProgressRepository ?? new vUserProgressRepository(_dimsDbContext);

        public IViewRepository<vUserTask> vUserTasks => _vUserTaskRepository ?? new vUserTaskRepository(_dimsDbContext);

        public IViewRepository<vUserTrack> vUserTracks => _vUserTrackRepository ?? new vUserTrackRepository(_dimsDbContext);

        public IvUserProfileRepository vUserProfiles => _vUserProfileRepository ?? new vUserProfileRepository(_dimsDbContext);

        public EFUnitOfWork(string connectionString)
        {
            this._dimsDbContext = new DIMSDBContext(connectionString);
        }
        public void Save()
        {
            _dimsDbContext.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //Release managed resources
                    _dimsDbContext.Dispose();
                }
                //release unmanaged resources
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
