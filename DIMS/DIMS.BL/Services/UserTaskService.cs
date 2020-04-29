using AutoMapper;
using HIMS.BL.DTO;
using HIMS.BL.Infrastructure;
using HIMS.BL.Interfaces;
using HIMS.EF.DAL.Data;
using HIMS.EF.DAL.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Services
{

    using DimsTask = global::HIMS.EF.DAL.Data.Task;

    public class UserTaskService : IUserTaskService
    {
        private IUnitOfWork Database;
        private UserTaskRepository Repository;

        public UserTaskService(IUnitOfWork uow, UserTaskRepository repository)
        {
            Database = uow;
            Repository = repository;
        }

        public void DeleteById(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The user task id value is not set", String.Empty);

            Database.UserTasks.DeleteById(id.Value);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public TaskDTO GetTask(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The user task id value is not set", String.Empty);

            return Mapper.Map<DimsTask, TaskDTO>(
                Database.UserTasks.GetById(id.Value).Task);
        }

        public TaskStateDTO GetTaskState(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The user task id value is not set", String.Empty);

            return Mapper.Map<TaskState, TaskStateDTO>(
                Database.UserTasks.GetById(id.Value).TaskState);
        }

        public IEnumerable<TaskTrackDTO> GetTaskTracks(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The user task id value is not set", String.Empty);

            return Mapper.Map<List<TaskTrack>, ICollection<TaskTrackDTO>>(
                Database.UserTasks.GetById(id.Value).TaskTracks.ToList());
        }

        public UserProfileDTO GetUserProfile(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The user task id value is not set", String.Empty);

            return Mapper.Map<UserProfile, UserProfileDTO>(
                Database.UserTasks.GetById(id.Value).UserProfile);
        }

        public UserTaskDTO GetById(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The user task id value is not set", String.Empty);

            var userTask = Database.UserTasks.GetById(id.Value);

            if (userTask == null)
                throw new ValidationException($"The user task with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<UserTask, UserTaskDTO>(userTask);
        }

        public IEnumerable<UserTaskDTO> GetAll()
        {
            return Mapper.Map<List<UserTask>, ICollection<UserTaskDTO>>(
                Database.UserTasks.GetAll().ToList());
        }

        public void Save(UserTaskDTO userTaskDTO)
        {
            var userTask = new UserTask
            {
                Task = Mapper.Map<TaskDTO, DimsTask>(userTaskDTO.Task),
                TaskId = userTaskDTO.TaskId,
                StateId = userTaskDTO.StateId,
                TaskState = Mapper.Map<TaskStateDTO, TaskState>(userTaskDTO.TaskState),
                UserId = userTaskDTO.UserId,
                UserProfile = Mapper.Map<UserProfileDTO, UserProfile>(userTaskDTO.UserProfile),
                TaskTracks = Mapper.Map<IEnumerable<TaskTrackDTO>, List<TaskTrack>>(userTaskDTO.TaskTracks)
            };

            Database.UserTasks.Create(userTask);
            Database.Save();
        }

        public void Update(UserTaskDTO userTaskDTO)
        {
            var userTask = Database.UserTasks.GetById(userTaskDTO.UserTaskId);

            if (userTask != null)
            {
                userTask.TaskId = userTaskDTO.TaskId;
                userTask.UserId = userTaskDTO.UserId;
                userTask.StateId = userTaskDTO.StateId;

                Database.Save();
            }
        }

        public IEnumerable<UserTaskDTO> GetByUserId(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The user profile id value is not set", String.Empty);

            return Mapper.Map<IEnumerable<UserTask>, IEnumerable<UserTaskDTO>>(
                Repository.GetByUserId(id.Value));
        }

        public async Task<bool> DeleteByIdAsync(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The id value is not set!", String.Empty);

            var userTask = await Database.UserTasks.DeleteByIdAsync(id.Value);

            if (userTask != null)
            {
                return true;
            }

            else
                return false;
        }

        public UserTaskDTO GetByTaskIdAndUserId(int? taskId, int? userId)
        {
            if (!taskId.HasValue || !userId.HasValue)
            {
                throw new ValidationException("Some of ids is not set!", String.Empty);
            }

            var userTask = Database.UserTasks.GetByTaskIdAndUserId(taskId.Value, userId.Value);

            if (userTask == null)
            {
                throw new ValidationException($"The user task with task id = {taskId.Value} and user id = {userId.Value} was not found", String.Empty);
            }

            return new UserTaskDTO
            {
                UserTaskId = userTask.UserTaskId,
                StateId = userTask.StateId,
                UserId = userTask.UserId,
                TaskId = userTask.TaskId
            };
        }
    }
}
