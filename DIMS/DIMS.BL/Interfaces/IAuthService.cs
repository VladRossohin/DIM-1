using System.Threading.Tasks;

namespace DIMS.BL.Interfaces
{
    public interface IAuthService<T>
    {
        Task<string> GenerateTokenAsync(T item);
    }
}
