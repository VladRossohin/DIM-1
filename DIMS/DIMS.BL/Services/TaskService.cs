using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Infrastructure;
using DIMS.BL.Interfaces;
using DIMS.EF.DAL.Data;
using DIMS.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS.BL.Services
{
    using DimsTask = EF.DAL.Data.Task;

    public class TaskService : ITaskService
    {

        private IUnitOfWork Database { get; }

        private readonly IMapper _mapper;

        public TaskService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }


        public void DeleteById(int? id)
        {
            if (!id.HasValue)
            {
                CustomLogger.Error("Error in Task Service", new ValidationException("The Task id value is not set", string.Empty));
            }

            Database.Tasks.DeleteById(id.Value);

            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public TaskDTO GetById(int id)
        {
            var task = Database.Tasks.GetById(id);

            if (task == null)
            {
                CustomLogger.Error("Error in Task Service", new ValidationException($"The task with id = {id} was not found", string.Empty));
            }

            return _mapper.Map<DimsTask, TaskDTO>(task);
        }

        public IEnumerable<TaskDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<DimsTask>, ICollection<TaskDTO>>(Database.Tasks.GetAll());
        }

        public IEnumerable<UserTaskDTO> GetUserTasks(int? id)
        {
            if (!id.HasValue)
            {
                CustomLogger.Error("Error in Task Service", new ValidationException("The task id value is not set", string.Empty));
            }

            return _mapper.Map<IEnumerable<UserTask>, ICollection<UserTaskDTO>>(Database.Tasks.
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
                UserTasks = _mapper.Map<List<UserTaskDTO>, ICollection<UserTask>>(task.UserTasks.ToList())
            };

            Database.Tasks.Create(_task);
            Database.Save();
        }

        public void Update(TaskDTO taskDTO)
        {
            var task = Database.Tasks.GetById(taskDTO.TaskId);

            if (task != null)
            {
                _mapper.Map(taskDTO, task);

                Database.Save();
            }
        }

        public async Task<bool> DeleteByIdAsync(int? id)
        {
            if (!id.HasValue)
            {
                CustomLogger.Error("Error in Task Service", new ValidationException("The task id value is not set", string.Empty));
            }

            var task = await Database.Tasks.DeleteByIdAsync(id.Value);

            return task != null ? true : false;
        }
    }
}
