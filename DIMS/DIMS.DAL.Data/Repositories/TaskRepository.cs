using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS.EF.DAL.Data.Repositories
{
    internal class TaskRepository : IRepository<Task>
    {
        private readonly DIMSDBContext _dIMSDBContext;

        public TaskRepository(DIMSDBContext dIMSDBContext)
        {
            _dIMSDBContext = dIMSDBContext;
        }

        public void Create(Task item)
        {
            _dIMSDBContext.Tasks.Add(item);
        }

        public void DeleteById(int id)
        {
            Task task = _dIMSDBContext.Tasks.Find(id);

            if (task != null)
            {
                _dIMSDBContext.Tasks.Remove(task);
            }
        }

        public async Task<Task> DeleteByIdAsync(int id)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var task = _dIMSDBContext.Tasks.Find(id);
                return _dIMSDBContext.Tasks.Remove(task);
            });
        }

        public IEnumerable<Task> Find(Func<Task, bool> predicate)
        {
            return _dIMSDBContext.Tasks.Where(predicate).ToList();
        }

        public Task GetById(int id)
        {
            return _dIMSDBContext.Tasks.Find(id);
        }

        public IEnumerable<Task> GetAll()
        {
            return _dIMSDBContext.Tasks;
        }

        public void Update(Task item)
        {
            _dIMSDBContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
