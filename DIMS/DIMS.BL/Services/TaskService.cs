using AutoMapper;
using HIMS.BL.DTO;
using HIMS.BL.Infrastructure;
using HIMS.BL.Interfaces;
using HIMS.EF.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Services
{

    using DimsTask = global::HIMS.EF.DAL.Data.Task;

    public class TaskService : ITaskService
    {

        private IUnitOfWork Database { get; }

        public TaskService (IUnitOfWork uow)
        {
            Database = uow;
        }


        public void DeleteById(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The Task id value is not set", String.Empty);

            Database.Tasks.DeleteById(id.Value);

            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public TaskDTO GetById(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The Task id value is not set", String.Empty);

            var task = Database.Tasks.GetById(id.Value);

            if (task == null)
                throw new ValidationException($"The task with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<DimsTask, TaskDTO>(task);
        }

        public IEnumerable<TaskDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<DimsTask>, ICollection<TaskDTO>>(Database.Tasks.GetAll());

        }

        public IEnumerable<UserTaskDTO> GetUserTasks(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The task id value is not set", String.Empty);

            return Mapper.Map<IEnumerable<UserTask>, ICollection<UserTaskDTO>>(Database.Tasks.
                GetById(id.Value).UserTasks);
        }

        public void Save(TaskDTO task)
        {
            var _task = new DimsTask
            {
                Name = task.Name,
                Description = task.Description,
                StartDate = task.StartDate,
                DeadlineDate = task.DeadlineDate,
                UserTasks = Mapper.Map<List<UserTaskDTO>, ICollection<UserTask>>(task.UserTasks.ToList())
            };

            Database.Tasks.Create(_task);
            Database.Save();
            
        }

        public void Update(TaskDTO taskDTO)
        {
            var task = Database.Tasks.GetById(taskDTO.TaskId);

            if (task != null)
            {

                task.Name = taskDTO.Name;
                task.Description = taskDTO.Description;
                task.StartDate = taskDTO.StartDate;
                task.DeadlineDate = taskDTO.DeadlineDate;

                Database.Save();

            }
        }

        public async Task<bool> DeleteByIdAsync(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The id value is not set!", String.Empty);
            }

            var task = await Database.Tasks.DeleteByIdAsync(id.Value);

            if (task != null)
            {
                return true;
            }

            else
                return false;
        }
    }
}
