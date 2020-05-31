using System.Web;

namespace DIMS.Server.utils
{
    public static class CurrentUser
    {
        public static bool IsAdmin => HttpContext.Current.GetUserObject()?.IsAdmin == true;

        public static UserObject GetUserObject(this HttpContext current)
        {
            return current != null ? (UserObject)current.Session["__userObject"] : null;
        }
    }

    public class UserObject
    {
        public bool IsAdmin { get; set; }
    }
}