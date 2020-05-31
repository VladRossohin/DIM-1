using System.Threading.Tasks;

namespace DIMS.BL.Interfaces
{
    public interface IService<T> : IVService<T>
    {
        void Save(T item);
        void Update(T item);
        void DeleteById(int? id);

        Task<bool> DeleteByIdAsync(int? id);

    }
}
