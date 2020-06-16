using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS.EF.DAL.Data.Repositories
{
    internal class TaskTrackRepository : IRepository<TaskTrack>
    {
        private readonly DIMSDBContext _dIMSDBContext;

        public TaskTrackRepository(DIMSDBContext dIMSDBContext)
        {
            _dIMSDBContext = dIMSDBContext;
        }

        public void Create(TaskTrack item)
        {
            _dIMSDBContext.TaskTracks.Add(item);
        }

        public void DeleteById(int id)
        {
            TaskTrack taskTrack = _dIMSDBContext.TaskTracks.Find(id);

            if (taskTrack != null)
            {
                _dIMSDBContext.TaskTracks.Remove(taskTrack);
            }
        }

        public async Task<TaskTrack> DeleteByIdAsync(int id)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var taskTrack = _dIMSDBContext.TaskTracks.Find(id);
                return _dIMSDBContext.TaskTracks.Remove(taskTrack);
            });
        }

        public IEnumerable<TaskTrack> Find(Func<TaskTrack, bool> predicate)
        {
            return _dIMSDBContext.TaskTracks.Where(predicate).ToList();
        }

        public TaskTrack GetById(int id)
        {
            return _dIMSDBContext.TaskTracks.Find(id);
        }

        public IEnumerable<TaskTrack> GetAll()
        {
            return _dIMSDBContext.TaskTracks;
        }

        public void Update(TaskTrack item)
        {
            _dIMSDBContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<vUserTrack> GetByUserId(int id)
        {
            return _dIMSDBContext.vUserTracks.Where(task => task.UserId == id).ToList();
        }
    }
}
