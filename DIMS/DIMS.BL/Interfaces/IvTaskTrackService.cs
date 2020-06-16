using DIMS.BL.DTO;

namespace DIMS.BL.Interfaces
{
    public interface IVTaskTrackService : IVService<VTaskTrackDTO>
    {
        void Update(VTaskTrackDTO vTaskTrackDTO);
    }
}
