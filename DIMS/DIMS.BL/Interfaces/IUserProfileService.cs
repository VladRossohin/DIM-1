﻿using DIMS.BL.DTO;
using System.Threading.Tasks;

namespace DIMS.BL.Interfaces
{
    public interface IUserProfileService : IService<UserProfileDTO>
    {
        Task<bool> DeleteByEmailAsync(string email);
    }
}
