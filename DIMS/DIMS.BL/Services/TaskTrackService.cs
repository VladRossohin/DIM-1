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
    public class TaskTrackService : ITaskTrackService
    {

        private readonly IUnitOfWork Database;
        private readonly IMapper _mapper;

        public TaskTrackService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        public void DeleteById(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The task track id value is not set", string.Empty);
            }

            Database.TaskTracks.DeleteById(id.Value);
            Database.Save();
        }

        public async Task<bool> DeleteByIdAsync(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The id value is not set!", string.Empty);
            }

            var taskTrack = await Database.TaskTracks.DeleteByIdAsync(id.Value);

            if (taskTrack != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public TaskTrackDTO GetById(int id)
        {
            var task = Database.TaskTracks.GetById(id);

            if (task == null)
            {
                throw new ValidationException($"The task track with id = {id} was not found", string.Empty);
            }

            return _mapper.Map<TaskTrack, TaskTrackDTO>(task);
        }

        public IEnumerable<TaskTrackDTO> GetAll()
        {
            return _mapper.Map<List<TaskTrack>, ICollection<TaskTrackDTO>>(
                Database.TaskTracks.GetAll().ToList());
        }

        public UserTaskDTO GetUserTask(int id)
        {
            return _mapper.Map<UserTask, UserTaskDTO>(
                Database.TaskTracks.GetById(id).UserTask);
        }

        public void Save(TaskTrackDTO taskTrackDTO)
        {
            var taskTrack = new TaskTrack
            {
                TrackNote = taskTrackDTO.TrackNote,
                TrackDate = taskTrackDTO.TrackDate,
                UserTask = _mapper.Map<UserTaskDTO, UserTask>(taskTrackDTO.UserTask),
                UserTaskId = taskTrackDTO.UserTaskId
            };

            Database.TaskTracks.Create(taskTrack);
            Database.Save();
        }

        public void Update(TaskTrackDTO taskTrackDTO)
        {
            var taskTrack = Database.TaskTracks.GetById(taskTrackDTO.TaskTrackId);

            if (taskTrack != null)
            {
                _mapper.Map(taskTrackDTO, taskTrack);
                Database.Save();
            }
        }

        public IEnumerable<TaskTrackDTO> GetTracksForUser(int userId)
        {
            var tracks = Database.VUserTracks.Find(item => item.UserId == userId);

            if (tracks == null)
            {
                throw new ValidationException($"The Task Track with id = {userId} was not found", "");
            }

            return _mapper.Map<IEnumerable<vUserTrack>, List<TaskTrackDTO>>(tracks);
        }
    }
}
