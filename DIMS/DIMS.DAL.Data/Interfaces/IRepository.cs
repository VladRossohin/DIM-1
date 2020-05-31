using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS.EF.DAL.Data
{
    public interface IRepository<T>
    {
        T GetById(int id);
        void Create(T item);
        void Update(T item);
        void DeleteById(int id);

        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T, bool> predicate);

        Task<T> DeleteByIdAsync(int id);
    }
}
