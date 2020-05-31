namespace DIMS.EF.DAL.Data.Interfaces
{
    public interface IUserTaskRepository : IRepository<UserTask>
    {
        void Delete(int userId, int taskId);
    }
}
