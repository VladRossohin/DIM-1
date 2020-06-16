using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Infrastructure;
using DIMS.BL.Interfaces;
using DIMS.EF.DAL.Data;
using DIMS.Logger;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DIMS.BL.Services
{
    public class VTaskTrackService : IVTaskTrackService
    {

        private IUnitOfWork Uow { get; }

        private readonly IMapper _mapper;

        public VTaskTrackService(IUnitOfWork uow, IMapper mapper)
        {
            Uow = uow;
            _mapper = mapper;
        }

        public void Dispose()
        {
            Uow.Dispose();
        }

        public VTaskTrackDTO GetById(int id)
        {
            var taskTrack = Uow.TaskTracks.GetById(id);

            if (taskTrack == null)
            {
                CustomLogger.Error("Error in VTaskTrack", new ValidationException($"The TaskTrack with id = {id} was not found", String.Empty));
            }
            return _mapper.Map<TaskTrack, VTaskTrackDTO>(taskTrack);
        }

        public IEnumerable<VTaskTrackDTO> GetAll()
        {
            var taskTrackDto = Uow.TaskTracks.GetAll().ToList();

            return _mapper.Map<List<TaskTrack>, ICollection<VTaskTrackDTO>>(taskTrackDto);
        }

        public void Update(VTaskTrackDTO vTaskTrackDTO)
        {
            var taskTrack = Uow.TaskTracks.GetById(vTaskTrackDTO.TaskTrackId);

            if (taskTrack != null)
            {
                _mapper.Map(vTaskTrackDTO, taskTrack);

                Uow.Save();
            }
        }
    }
}
