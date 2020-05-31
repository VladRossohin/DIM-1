using AutoMapper;
using DIMS.BL.Models;

namespace DIMS.Server.Models
{
    [AutoMap(typeof(SampleDTO))]
    public class SampleViewModel
    {
        public int SampleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}