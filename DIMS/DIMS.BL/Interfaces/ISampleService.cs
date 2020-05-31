using DIMS.BL.Models;
using System.Collections.Generic;

namespace DIMS.BL.Interfaces
{
    public interface ISampleService
    {
        void SaveSample(SampleDTO sampleTM);
        SampleDTO GetSample(int id);
        void UpdateSample(SampleDTO sampleDTO);
        void DeleteSample(int? id);

        IEnumerable<SampleDTO> GetSamples();
        int GetSampleEntriesAmout(bool isAdmin);
        void Dispose();
    }
}
