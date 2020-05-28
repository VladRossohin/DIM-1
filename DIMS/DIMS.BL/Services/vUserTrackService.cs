﻿using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Infrastructure;
using DIMS.BL.Interfaces;
using DIMS.EF.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DIMS.BL.Services
{
    public class VUserTrackService : IVUserTrackService
    {

        private readonly IUnitOfWork Database;

        private readonly IMapper _mapper;

        public VUserTrackService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public vUserTrackDTO GetById(int id)
        {
            var _vUserTrack = Database.VUserTracks.GetById(id);

            if (_vUserTrack == null)
            {
                throw new ValidationException($"The view user track with id = {id} was not found", String.Empty);
            }

            return _mapper.Map<vUserTrack, vUserTrackDTO>(_vUserTrack);
        }

        public IEnumerable<vUserTrackDTO> GetAll()
        {
            return _mapper.Map<List<vUserTrack>, ICollection<vUserTrackDTO>>(
                Database.VUserTracks.GetAll().ToList());
        }
    }
}
