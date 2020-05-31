using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS.EF.DAL.Data.Repositories
{
    public class DirectionRepository : IRepository<Direction>
    {
        private readonly DIMSDBContext _dIMSDBContext;

        public DirectionRepository(DIMSDBContext dIMSDBContext)
        {
            _dIMSDBContext = dIMSDBContext;
        }

        public void Create(Direction item)
        {
            _dIMSDBContext.Directions.Add(item);
        }

        public void DeleteById(int id)
        {
            var direction = _dIMSDBContext.Directions.Find(id);

            if (direction != null)
            {
                _dIMSDBContext.Directions.Remove(direction);
            }
        }

        public async Task<Direction> DeleteByIdAsync(int id)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var direction = _dIMSDBContext.Directions.Find(id);

                return _dIMSDBContext.Directions.Remove(direction);
            });
        }

        public IEnumerable<Direction> Find(Func<Direction, bool> predicate)
        {
            return _dIMSDBContext.Directions.Where(predicate).ToList();
        }

        public Direction GetById(int id)
        {
            return _dIMSDBContext.Directions.Find(id);
        }

        public IEnumerable<Direction> GetAll()
        {
            return _dIMSDBContext.Directions;
        }

        public void Update(Direction item)
        {
            _dIMSDBContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
