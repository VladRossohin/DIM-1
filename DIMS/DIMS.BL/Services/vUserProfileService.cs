﻿using AutoMapper;
using DIMS.BL.DTO;
using DIMS.BL.Infrastructure;
using DIMS.BL.Interfaces;
using DIMS.EF.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS.BL.Services
{
    public class VUserProfileService : IVUserProfileService
    {

        private readonly IUnitOfWork Database;

        private readonly IMapper _mapper;

        public VUserProfileService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public vUserProfileDTO GetById(int id)
        {
            var vUserProfile = Database.VUserProfiles.GetById(id);

            if (vUserProfile == null)
            {
                throw new ValidationException($"The view user profile with id = {id} was not found", String.Empty);
            }

            return _mapper.Map<vUserProfile, vUserProfileDTO>(vUserProfile);
        }

        public async Task<vUserProfileDTO> GetByEmailAsync(string email)
        {
            if (email == null)
            {
                throw new ValidationException("The view user profile email is not set", String.Empty);
            }

            var vUserProfile = await Database.VUserProfiles.GetByEmailAsync(email);

            if (vUserProfile == null)
            {
                throw new ValidationException($"The view user profile with email = {email} was not found", String.Empty);
            }

            return _mapper.Map<vUserProfile, vUserProfileDTO>(vUserProfile);
        }

        public IEnumerable<vUserProfileDTO> GetAll()
        {
            return _mapper.Map<List<vUserProfile>, ICollection<vUserProfileDTO>>(
                Database.VUserProfiles.GetAll().ToList());
        }
    }
}
