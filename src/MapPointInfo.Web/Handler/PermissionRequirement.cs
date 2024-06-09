using Microsoft.AspNetCore.Authorization;

namespace MapPointInfo.Web.Handler
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement()
        {
        }
    }
}
