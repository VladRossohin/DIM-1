using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Infrastructure;
using DIMS.BL.Interfaces;
using DIMS.EF.DAL.Data;
using DIMS.EF.DAL.Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace DIMS.BL.Services
{
    public class VUserTaskService : IVUserTaskService
    {

        private readonly IUnitOfWork Database;
        private readonly VUserTaskRepository Repository;
        private readonly IMapper _mapper;

        public VUserTaskService(IUnitOfWork uow, VUserTaskRepository repository, IMapper mapper)
        {
            Database = uow;
            Repository = repository;
            _mapper = mapper;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public IEnumerable<VUserTaskDTO> GetByUserId(int id)
        {
            var vUserTaskDtos = Repository.GetByUserId(id);

            return _mapper.Map<IEnumerable<vUserTask>, IEnumerable<VUserTaskDTO>>(vUserTaskDtos);
        }

        public VUserTaskDTO GetById(int id)
        {

            var _vUserTask = Database.VUserTasks.GetById(id);

            if (_vUserTask == null)
            {
                throw new ValidationException($"The view user task with id = {id} was not found", string.Empty);
            }

            return _mapper.Map<vUserTask, VUserTaskDTO>(_vUserTask);
        }

        public IEnumerable<VUserTaskDTO> GetAll()
        {
            var vUserTasks = Database.VUserTasks.GetAll().ToList();

            return _mapper.Map<List<vUserTask>, ICollection<VUserTaskDTO>>(vUserTasks);
        }

        public void Save(VUserTaskDTO vUserTaskDTO)
        {
            var userTask = new UserTask
            {
                TaskId = vUserTaskDTO.TaskId,
                StateId = vUserTaskDTO.StateId,
                UserId = vUserTaskDTO.UserId
            };

            Database.UserTasks.Create(userTask);
            Database.Save();
        }

        public void Update(VUserTaskDTO vUserTaskDTO)
        {
            var userTask = Database.UserTasks.GetById(vUserTaskDTO.UserTaskId);

            if (userTask != null)
            {
                _mapper.Map(vUserTaskDTO, userTask);
                Database.Save();
            }
        }
    }
}
