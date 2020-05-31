using DIMS.BL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Email.Interfaces
{
    public interface ISender
    {
        Task<string> MessageToUserAsync(UserDTO user, string subject, string html);

        Task MessageToUserAsync(IEnumerable<UserDTO> users, string subject, string html);
    }
}