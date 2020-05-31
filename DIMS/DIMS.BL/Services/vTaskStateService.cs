using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Infrastructure;
using DIMS.BL.Interfaces;
using DIMS.EF.DAL.Data;
using System;
using System.Collections.Generic;

namespace DIMS.BL.Services
{
    public class VTaskStateService : IVTaskStateService
    {
        private IUnitOfWork Uow { get; }

        private readonly IMapper _mapper;

        public VTaskStateService(IUnitOfWork uow, IMapper mapper)
        {
            Uow = uow;
            _mapper = mapper;
        }

        public TaskStateDTO GetById(int id)
        {
            var task = Uow.TaskStates.GetById(id);

            if (task == null)
            {
                throw new ValidationException($"The task state with id = {id} was not found", String.Empty);
            }

            return _mapper.Map<TaskState, TaskStateDTO>(task);

        }

        public void Dispose()
        {
            Uow.Dispose();
        }

        public IEnumerable<TaskStateDTO> GetAll()
        {
            var taskStates = Uow.TaskStates.GetAll();

            return _mapper.Map<IEnumerable<TaskState>, IEnumerable<TaskStateDTO>>(taskStates);
        }
    }
}
