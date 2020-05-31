using DIMS.Common;
using DIMS.EF.DAL.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ThreadTask = System.Threading.Tasks.Task;

namespace DIMS.EF.DAL.Data.Repositories
{
    public class UserTaskRepository : IUserTaskRepository
    {
        private readonly DIMSDBContext _dIMSDBContext;

        public UserTaskRepository(DIMSDBContext dIMSDBContext)
        {
            _dIMSDBContext = dIMSDBContext;
        }

        public void Create(UserTask item)
        {
            _dIMSDBContext.UserTasks.Add(item);
        }

        public void DeleteById(int id)
        {
            UserTask userTask = _dIMSDBContext.UserTasks.Find(id);

            if (userTask != null)
            {
                _dIMSDBContext.UserTasks.Remove(userTask);
            }
        }

        public void Delete(int taskId, int userId)
        {
            var userTask = _dIMSDBContext.UserTasks.FirstOrDefault(ut => ut.UserId == userId && ut.TaskId == taskId);

            if (userTask != null)
            {
                _dIMSDBContext.UserTasks.Remove(userTask);
            }
        }

        public IEnumerable<UserTask> Find(Func<UserTask, bool> predicate)
        {
            return _dIMSDBContext.UserTasks.Where(predicate).ToList();
        }

        public UserTask GetById(int id)
        {
            return _dIMSDBContext.UserTasks.Find(id);
        }

        public IEnumerable<UserTask> GetByUserId(int id)
        {
            // 1
            //return _dIMSDBContext.UserTasks.Where(task => task.UserId == id).ToList();

            // 2
            return _dIMSDBContext.UserTasks.ToListBy(task => task.UserId == id);
        }

        public IEnumerable<UserTask> GetAll()
        {
            return _dIMSDBContext.UserTasks;
        }

        public void Update(UserTask item)
        {
            _dIMSDBContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task<UserTask> DeleteByIdAsync(int id)
        {
            return await ThreadTask.Run(() =>
            {
                var userTask = _dIMSDBContext.UserTasks.Find(id);
                return _dIMSDBContext.UserTasks.Remove(userTask);
            });
        }
    }
}
