using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS.EF.DAL.Data.Repositories
{
    internal class TaskStateRepository : IRepository<TaskState>
    {
        private readonly DIMSDBContext _dIMSDBContext;

        public TaskStateRepository(DIMSDBContext dIMSDBContext)
        {
            _dIMSDBContext = dIMSDBContext;
        }

        public void Create(TaskState item)
        {
            _dIMSDBContext.TaskStates.Add(item);
        }

        public void DeleteById(int id)
        {
            TaskState taskState = _dIMSDBContext.TaskStates.Find(id);

            if (taskState != null)
            {
                _dIMSDBContext.TaskStates.Remove(taskState);
            }
        }

        public async Task<TaskState> DeleteByIdAsync(int id)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var taskState = _dIMSDBContext.TaskStates.Find(id);

                return _dIMSDBContext.TaskStates.Remove(taskState);
            });
        }

        public IEnumerable<TaskState> Find(Func<TaskState, bool> predicate)
        {
            return _dIMSDBContext.TaskStates.Where(predicate).ToList();
        }

        public TaskState GetById(int id)
        {
            return _dIMSDBContext.TaskStates.Find(id);
        }

        public IEnumerable<TaskState> GetAll()
        {
            return _dIMSDBContext.TaskStates;
        }

        public void Update(TaskState item)
        {
            _dIMSDBContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
