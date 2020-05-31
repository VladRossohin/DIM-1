using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Infrastructure;
using DIMS.BL.Interfaces;
using DIMS.EF.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DIMS.BL.Services
{
    public class VUserProgressService : IVUserProgressService
    {
        private readonly IUnitOfWork Database;

        private readonly IMapper _mapper;

        public VUserProgressService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public IEnumerable<VUserProgressDTO> GetAll()
        {
            return _mapper.Map<List<vUserProgress>, ICollection<VUserProgressDTO>>(
                Database.VUserProgresses.GetAll().ToList());
        }

        public VUserProgressDTO GetById(int id)
        {
            var _vUserProgress = Database.VUserProgresses.GetById(id);

            if (_vUserProgress == null)
            {
                throw new ValidationException($"The view user progress with id = {id} was not found", String.Empty);
            }

            return _mapper.Map<vUserProgress, VUserProgressDTO>(_vUserProgress);

        }

        public IEnumerable<VUserProgressDTO> GetByUserId(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The view user progress id value is not set", String.Empty);
            }

            var userProgress = Database.VUserProgresses.Find(m => m.UserId == id.Value);

            if (userProgress == null)
            {
                throw new ValidationException($"The user with id = {id.Value} was not found", String.Empty);
            }

            return _mapper.Map<IEnumerable<vUserProgress>, List<VUserProgressDTO>>(userProgress);
        }
    }
}
