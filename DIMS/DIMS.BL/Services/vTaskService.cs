using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Infrastructure;
using DIMS.BL.Interfaces;
using DIMS.EF.DAL.Data;
using System.Collections.Generic;
using System.Linq;

namespace DIMS.BL.Services
{
    public class VTaskService : IVTaskService
    {

        private readonly IUnitOfWork Database;
        private readonly IMapper _mapper;

        public VTaskService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public VTaskDTO GetById(int id)
        {
            var _vTask = Database.VTasks.GetById(id);

            if (_vTask == null)
            {
                throw new ValidationException($"The vTask with id = {id} was not found", string.Empty);
            }

            return _mapper.Map<vTask, VTaskDTO>(_vTask);
        }

        public IEnumerable<VTaskDTO> GetAll()
        {
            return _mapper.Map<List<vTask>, ICollection<VTaskDTO>>(
                Database.VTasks.GetAll().ToList());
        }

        public void Update(VTaskDTO vTaskDTO)
        {
            var task = Database.Tasks.GetById(vTaskDTO.TaskId);

            if (task != null)
            {
                _mapper.Map(vTaskDTO, task);

                Database.Save();
            }
        }
    }
}
