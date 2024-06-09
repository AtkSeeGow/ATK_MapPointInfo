using MapPointInfo.Service;
using MapPointInfo.Web.Handler;
using Microsoft.AspNetCore.Authorization;

namespace MapPointInfo.Web.Handler
{
    public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly AuthorizationService authorizationService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public PermissionRequirementHandler(
            AuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor)
        {
            this.authorizationService = authorizationService;
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            return Task.CompletedTask;
        }
    }
}
