﻿using HIMS.BL.DTO;
using HIMS.EF.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Interfaces
{
    public interface IvUserTrackService : IvService<vUserTrackDTO>
    {
        IEnumerable<vUserTrackDTO> GetTracksForUser(int userId);
    }
}
