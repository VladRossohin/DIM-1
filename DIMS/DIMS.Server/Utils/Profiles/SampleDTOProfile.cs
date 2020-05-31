using AutoMapper;
using DIMS.BL.Models;
using DIMS.EF.DAL.Data;
using System.Collections.Generic;

namespace DIMS.BL.DTO
{
    internal class SampleDTOProfile : Profile
    {
        public SampleDTOProfile()
        {
            CreateMap<SampleDTO, Sample>();
            CreateMap<Sample, SampleDTO>();
            CreateMap<IEnumerable<Sample>, List<SampleDTO>>();
        }
    }
}
