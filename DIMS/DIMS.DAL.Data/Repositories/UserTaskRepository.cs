﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.EF.DAL.Data.Repositories
{
    public class UserTaskRepository : IRepository<UserTask>
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

        public void Delete(int id)
        {
            UserTask userTask = _dIMSDBContext.UserTasks.Find(id);
            
            if (userTask != null)
            {
                _dIMSDBContext.UserTasks.Remove(userTask);
            }
        }

        public IEnumerable<UserTask> Find(Func<UserTask, bool> predicate)
        {
            return _dIMSDBContext.UserTasks.Where(predicate).ToList();
        }

        public UserTask Get(int id)
        {
            return _dIMSDBContext.UserTasks.Find(id);
        }

        public IEnumerable<UserTask> GetByUserId(int id)
        {
            return _dIMSDBContext.UserTasks.Where(task => task.UserId == id).ToList();
        } 

        public IEnumerable<UserTask> GetAll()
        {
            return _dIMSDBContext.UserTasks;
        }

        public void Update(UserTask item)
        {
            _dIMSDBContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task<UserTask> DeleteAsync(int id)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var ut = _dIMSDBContext.UserTasks.Find(id);
                return _dIMSDBContext.UserTasks.Remove(ut);
            });
        }
    }
}
