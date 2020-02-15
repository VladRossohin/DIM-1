﻿using AutoMapper;
using HIMS.BL.DTO;
using HIMS.BL.Infrastructure;
using HIMS.BL.Interfaces;
using HIMS.EF.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Services
{
    public class vUserProfileService : IvUserProfileService
    {

        private IUnitOfWork Database;

        public vUserProfileService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public vUserProfileDTO GetVUserProfile(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The view user profile id value is not set", String.Empty);

            var _vUserProfile = Database.vUserProfiles.Get(id.Value);

            if (_vUserProfile == null)
                throw new ValidationException($"The view user profile with id = {id.Value} was not found", String.Empty);

            return Mapper.Map<vUserProfile, vUserProfileDTO>(_vUserProfile);
        }

        public ICollection<vUserProfileDTO> GetVUserProfiles()
        {
            return Mapper.Map<List<vUserProfile>, ICollection<vUserProfileDTO>>(
                Database.vUserProfiles.GetAll().ToList());
        }
    }
}