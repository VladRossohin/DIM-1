using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS.EF.DAL.Data.Repositories
{
    public class SampleRepository : IRepository<Sample>
    {
        private readonly DIMSDBContext _dimsDbContext;

        public SampleRepository(DIMSDBContext dimsDbContext)
        {
            _dimsDbContext = dimsDbContext;
        }

        public void Create(Sample item)
        {
            _dimsDbContext.Samples.Add(item);
        }

        public void DeleteById(int id)
        {
            var entity = _dimsDbContext.Samples.Find(id);
            if (entity != null)
            {
                _dimsDbContext.Samples.Remove(entity);
            }
        }

        public async Task<Sample> DeleteByIdAsync(int id)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var sample = _dimsDbContext.Samples.Find(id);

                return _dimsDbContext.Samples.Remove(sample);
            });
        }

        public IEnumerable<Sample> Find(Func<Sample, bool> predicate)
        {
            return _dimsDbContext.Samples.Where(predicate).ToList();
        }

        public Sample GetById(int id)
        {
            return _dimsDbContext.Samples.Find(id);
        }

        public IEnumerable<Sample> GetAll()
        {
            return _dimsDbContext.Samples;
        }

        public void Update(Sample item)
        {
            _dimsDbContext.Entry(item).State = EntityState.Modified;
        }
    }
}
