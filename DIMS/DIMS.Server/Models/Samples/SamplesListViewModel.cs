using System.Collections.Generic;

namespace DIMS.Server.Models
{
    public class SamplesListViewModel
    {
        public IEnumerable<SampleViewModel> Samples { get; set; }
        public int? SamplesAmount { get; set; }
    }
}