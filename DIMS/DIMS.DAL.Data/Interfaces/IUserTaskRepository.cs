using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.EF.DAL.Data.Interfaces
{
    public interface IUserTaskRepository : IRepository<UserTask>
    {
        UserTask GetByTaskIdAndUserId(int taskId, int userId);
    }
}
