using MapPointInfo.Domain;
using MapPointInfo.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MapPointInfo.Web
{
    [Route("Api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "PermissionHandler")]
    public class AuthenticationController : Controller
    {
        private readonly AuthorizationService authorizationService;

        public AuthenticationController(AuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult GenerateToken([FromBody] dynamic userInformation)
        {
            var messages = new Messages();

            var account = userInformation.account != null ? (string)userInformation.account : string.Empty;
            var password = userInformation.password != null ? (string)userInformation.password : string.Empty;
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                messages.ErrorMessages.Add(Guid.NewGuid().ToString(), "使用者名稱或密碼不能為空白");
                return StatusCode(StatusCodes.Status400BadRequest, messages);
            }

            var validateCredentialsResult = this.authorizationService.ValidateCredentials(account, password);

            if (!validateCredentialsResult)
            {
                messages.ErrorMessages.Add(Guid.NewGuid().ToString(), "使用者名稱或密碼錯誤");
                return StatusCode(StatusCodes.Status400BadRequest, messages);
            }

            var token = this.authorizationService.GenerateToken(account);
            Response.Cookies.Append("Authorization", token);

            return Ok(messages);
        }

        [HttpPost]
        public ActionResult CanActivate([FromBody] dynamic functionKeys)
        {
            return Ok(true);
        }
    }
}
