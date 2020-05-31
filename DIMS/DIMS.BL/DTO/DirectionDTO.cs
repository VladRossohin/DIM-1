using AutoMapper;
using DIMS.EF.DAL.Data;
using System.Collections.Generic;

namespace DIMS.BL.DTO
{
    [AutoMap(typeof(Direction))]
    public class DirectionDTO
    {
        public DirectionDTO()
        {
            UserProfiles = new HashSet<UserProfileDTO>();
        }

        public int DirectionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual IEnumerable<UserProfileDTO> UserProfiles { get; set; }
    }
}
