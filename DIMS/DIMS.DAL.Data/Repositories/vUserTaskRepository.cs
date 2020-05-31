using DIMS.EF.DAL.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DIMS.EF.DAL.Data.Repositories
{
    public class VUserTaskRepository : IViewRepository<vUserTask>
    {

        private readonly DIMSDBContext _dIMSDBContext;

        public VUserTaskRepository(DIMSDBContext dIMSDBContext)
        {
            _dIMSDBContext = dIMSDBContext;
        }

        public IEnumerable<vUserTask> Find(Func<vUserTask, bool> predicate)
        {
            return _dIMSDBContext.vUserTasks.Where(predicate).ToList();
        }

        public vUserTask GetById(int id)
        {
            return _dIMSDBContext.vUserTasks.Find(id);
        }

        public IEnumerable<vUserTask> GetByUserId(int id)
        {
            return _dIMSDBContext.vUserTasks.Where(vUserTask => vUserTask.UserId == id).ToList();
        }

        public IEnumerable<vUserTask> GetAll()
        {
            return _dIMSDBContext.vUserTasks;
        }
    }
}
