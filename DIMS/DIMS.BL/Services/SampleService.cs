using AutoMapper;
using DIMS.BL.Infrastructure;
using DIMS.BL.Interfaces;
using DIMS.BL.Models;
using DIMS.EF.DAL.Data;
using DIMS.EF.DAL.Data.Interfaces;
using System.Collections.Generic;

namespace DIMS.BL.Services
{
    public class SampleService : ISampleService
    {
        private IUnitOfWork Database { get; }
        private IProcedureManager Pm { get; }

        private readonly IMapper _mapper;

        public SampleService(IUnitOfWork uow, IProcedureManager pm, IMapper mapper)
        {
            Database = uow;
            Pm = pm;
            _mapper = mapper;
        }
        public void SaveSample(SampleDTO sampleDto)
        {
            // Validation
            if (sampleDto.Name.Length > 25)
            {
                throw new ValidationException($"The length of {nameof(sampleDto.Name)} must be less then 25"
                    , nameof(sampleDto.Name));
            }

            if (sampleDto.Description.Length > 255)
            {
                throw new ValidationException($"The length of {nameof(sampleDto.Description)} must be less then 25"
                    , nameof(sampleDto.Description));
            }

            var sample = new Sample
            {
                Name = sampleDto.Name,
                Description = sampleDto.Description
            };

            Database.Samples.Create(sample);
            Database.Save();
        }
        public void UpdateSample(SampleDTO sampleDTO)
        {
            // Validation
            if (sampleDTO.Name.Length > 25)
            {
                throw new ValidationException($"The length of {nameof(sampleDTO.Name)} must be less then 25"
                    , nameof(sampleDTO.Name));
            }

            if (sampleDTO.Description.Length > 255)
            {
                throw new ValidationException($"The length of {nameof(sampleDTO.Description)} must be less then 25"
                    , nameof(sampleDTO.Description));
            }

            var sample = Database.Samples.GetById(sampleDTO.SampleId);

            if (sample != null)
            {
                _mapper.Map(sampleDTO, sample);
                Database.Save();
            }
        }

        public IEnumerable<SampleDTO> GetSamples()
        {
            var samples = Database.Samples.GetAll();

           return _mapper.Map<IEnumerable<Sample>, List<SampleDTO>>(samples);
        }

        public SampleDTO GetSample(int id)
        {
            var sample = Database.Samples.GetById(id);

            if (sample == null)
            {
                throw new ValidationException($"The Sample with id = {id} was not found", string.Empty);
            }

            return _mapper.Map<Sample, SampleDTO>(sample);
        }

        public void DeleteSample(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("The Sample's id value is not set", string.Empty);
            }

            Database.Samples.DeleteById(id.Value);
            Database.Save();
        }

        public void SaveChanges()
        {
            Database.Save();
        }

        public int GetSampleEntriesAmout(bool isAdmin = false)
        {
            return Pm.GetSampleEntriesAmount(isAdmin);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
