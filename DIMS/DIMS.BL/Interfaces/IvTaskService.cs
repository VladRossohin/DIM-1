using DIMS.BL.DTO;

namespace DIMS.BL.Interfaces
{
    public interface IVTaskService : IVService<VTaskDTO>
    {
        void Update(VTaskDTO vTaskDTO);
    }
}
