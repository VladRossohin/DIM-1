using HIMS.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace HIMS.Tests.Controller
{
    public class MemberFakeController : ApiController
    {
        List<UserProfileViewModel> userProfiles = new List<UserProfileViewModel>();

        public MemberFakeController()
        {
        }

        public MemberFakeController(List<UserProfileViewModel> userProfiles)
        {
            this.userProfiles = userProfiles;
        }

        public IHttpActionResult GetUser(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var user = userProfiles.Where(userProfile => userProfile.UserId == id.Value).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        public IEnumerable<UserProfileViewModel> GetAllUsers()
        {
            return userProfiles;
        }
    }
}
