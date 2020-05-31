using AutoMapper;
using DIMS.BL.Models;

namespace DIMS.Server.Models.Samples
{
    public class SampleViewModelProfile : Profile
    {
        public SampleViewModelProfile()
        {
            CreateMap<SampleViewModel, SampleDTO>();
        }
    }
}