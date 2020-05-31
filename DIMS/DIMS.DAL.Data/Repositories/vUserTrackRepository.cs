using DIMS.EF.DAL.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DIMS.EF.DAL.Data.Repositories
{
    public class VUserTrackRepository : IViewRepository<vUserTrack>
    {

        private readonly DIMSDBContext _dIMSDBContext;

        public VUserTrackRepository(DIMSDBContext dIMSDBContext)
        {
            _dIMSDBContext = dIMSDBContext;
        }

        public IEnumerable<vUserTrack> Find(Func<vUserTrack, bool> predicate)
        {
            return _dIMSDBContext.vUserTracks.Where(predicate).ToList();
        }

        public vUserTrack GetById(int id)
        {
            return _dIMSDBContext.vUserTracks.Find(id);
        }

        public IEnumerable<vUserTrack> GetAll()
        {
            return _dIMSDBContext.vUserTracks;
        }
    }
}
