using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS.EF.DAL.Data.Repositories
{
    public class UserProfileRepository : IRepository<UserProfile>
    {
        private readonly DIMSDBContext _dimsDbContext;

        public UserProfileRepository(DIMSDBContext dIMSDBContext)
        {
            _dimsDbContext = dIMSDBContext;
        }

        public void Create(UserProfile userProfile)
        {
            _dimsDbContext.UserProfiles.Add(userProfile);
        }

        public void DeleteById(int id)
        {
            var profile = _dimsDbContext.UserProfiles.Find(id);

            if (profile != null)
            {
                _dimsDbContext.UserProfiles.Remove(profile);
            }
        }

        public void DeleteByEmail(string email)
        {
            var profile = _dimsDbContext.UserProfiles.Where(userProfile => userProfile.Email == email).FirstOrDefault();

            if (profile != null)
            {
                _dimsDbContext.UserProfiles.Remove(profile);
                _dimsDbContext.SaveChanges();
            }
        }

        public IEnumerable<UserProfile> Find(Func<UserProfile, bool> predicate)
        {
            return _dimsDbContext.UserProfiles.Where(predicate).ToList();
        }

        public UserProfile GetById(int id)
        {
            return _dimsDbContext.UserProfiles.Find(id);
        }

        public UserProfile GetByEmail(string email)
        {
            return _dimsDbContext.UserProfiles.Find(email);
        }

        public IEnumerable<UserProfile> GetAll()
        {
            return _dimsDbContext.UserProfiles;
        }

        public void Update(UserProfile userProfile)
        {
            _dimsDbContext.Entry(userProfile).State = System.Data.Entity.EntityState.Modified;
        }

        public async Task<UserProfile> DeleteByIdAsync(int id)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                var profile = _dimsDbContext.UserProfiles.Find(id);
                return _dimsDbContext.UserProfiles.Remove(profile);
            });
        }
    }
}
