using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Infrastructure;
using DIMS.BL.Interfaces;
using DIMS.EF.DAL.Data;
using DIMS.Logger;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS.BL.Services
{
    public class DirectionService : IDirectionService
    {
        private IUnitOfWork Database { get; }
        private readonly IMapper _mapper;

        public DirectionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            Database = unitOfWork;
            _mapper = mapper;
        }

        public void DeleteById(int? id)
        {
            if (!id.HasValue)
            {
                CustomLogger.Error("Error in Direction Service", new ValidationException("The Direction's id value is not set"));
            }

            Database.Directions.DeleteById(id.Value);
            Database.Save();
        }

        public async Task<bool> DeleteByIdAsync(int? id)
        {
            if (!id.HasValue)
            {
                CustomLogger.Error("Error in Direction Service", new ValidationException("The Direction's id value is not set", string.Empty));
            }

            var direction = await Database.Directions.DeleteByIdAsync(id.Value);

            if (direction != null)
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

        public DirectionDTO GetById(int id)
        {
            var direction = Database.Directions.GetById(id);

            if (direction == null)
            {
                CustomLogger.Error("Error in Direction Service", new ValidationException($"The Direction with id = ${id} was not found", string.Empty));
            }

            return _mapper.Map<Direction, DirectionDTO>(direction);
        }

        public IEnumerable<DirectionDTO> GetAll()
        {
            var directions = Database.Directions.GetAll();

            return _mapper.Map<IEnumerable<Direction>, List<DirectionDTO>>(directions);
        }

        public void Save(DirectionDTO direction)
        {
            var _direction = new Direction
            {
                Name = direction.Name,
                Description = direction.Description,
                UserProfiles = _mapper.Map<List<UserProfileDTO>, ICollection<UserProfile>>(direction.UserProfiles.ToList())
            };

            Database.Directions.Create(_direction);

            Database.Save();
        }

        public void Update(DirectionDTO direction)
        {
            var _direction = Database.Directions.GetById(direction.DirectionId);

            if (_direction != null)
            {
                _mapper.Map(direction, _direction);

                Database.Save();
            }
        }
    }
}
