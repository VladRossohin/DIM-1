using AutoMapper;
using DIMS.EF.DAL.Data;

namespace DIMS.BL.Models
{
    [AutoMap(typeof(Sample))]
    public class SampleDTO
    {
        public int SampleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
