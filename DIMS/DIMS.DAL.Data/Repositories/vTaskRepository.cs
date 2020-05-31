using DIMS.EF.DAL.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DIMS.EF.DAL.Data.Repositories
{
    public class VTaskRepository : IViewRepository<vTask>
    {
        private readonly DIMSDBContext _dIMSDBContext;

        public VTaskRepository(DIMSDBContext dIMSDBContext)
        {
            _dIMSDBContext = dIMSDBContext;
        }

        public IEnumerable<vTask> Find(Func<vTask, bool> predicate)
        {
            return _dIMSDBContext.vTasks.Where(predicate).ToList();
        }

        public vTask GetById(int id)
        {
            return _dIMSDBContext.vTasks.Find(id);
        }

        public IEnumerable<vTask> GetAll()
        {
            return _dIMSDBContext.vTasks;
        }
    }
}
