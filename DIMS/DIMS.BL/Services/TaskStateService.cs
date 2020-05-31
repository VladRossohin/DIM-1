using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Infrastructure;
using DIMS.BL.Interfaces;
using DIMS.EF.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS.BL.Services
{
    public class TaskStateService : ITaskStateService
    {
        private IUnitOfWork Database { get; }

        private readonly IMapper _mapper;

        public TaskStateService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        public void DeleteById(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The task state id value is not set", string.Empty);
            }

            Database.TaskStates.DeleteById(id.Value);

            Database.Save();

        }

        public TaskStateDTO GetById(int id)
        {
            var task = Database.TaskStates.GetById(id);

            if (task == null)
            {
                throw new ValidationException($"The task state with id = {id} was not found", string.Empty);
            }

            return _mapper.Map<TaskState, TaskStateDTO>(task);

        }

        public IEnumerable<UserTaskDTO> GetUserTasks(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The task state id value is not set", string.Empty);
            }

            return _mapper.Map<List<UserTask>, ICollection<UserTaskDTO>>(
                Database.TaskStates.GetById(id.Value).UserTasks.ToList());

        }

        public void Save(TaskStateDTO taskStateDTO)
        {
            var taskState = new TaskState
            {
                StateName = taskStateDTO.StateName,
                UserTasks = _mapper.Map<List<UserTask>>(taskStateDTO.UserTasks)
            };

            Database.TaskStates.Create(taskState);
            Database.Save();
        }

        public void Update(TaskStateDTO taskStateDTO)
        {
            var taskState = Database.TaskStates.GetById(taskStateDTO.StateId);

            if (taskState != null)
            {
                _mapper.Map(taskStateDTO, taskState);

                Database.Save();
            }
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public IEnumerable<TaskStateDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<TaskState>, IEnumerable<TaskStateDTO>>(
                Database.TaskStates.GetAll());
        }

        public async Task<bool> DeleteByIdAsync(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The id value is not set!", string.Empty);
            }

            var taskState = await Database.TaskStates.DeleteByIdAsync(id.Value);

            if (taskState != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
