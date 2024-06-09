using System.DirectoryServices.AccountManagement;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MapPointInfo.Domain.Options;
using Microsoft.IdentityModel.Tokens;
using ContextType = System.DirectoryServices.AccountManagement.ContextType;

namespace MapPointInfo.Service
{
    public class AuthorizationService
    {
        private readonly AuthorizationOption authorizationOption;
        private readonly TokenOption tokenOption;

        public static readonly string AuthorizationTokenKey = "Authorization";

        public AuthorizationService(AuthorizationOption authorizationOption, TokenOption tokenOption)
        {
            this.authorizationOption = authorizationOption;
            this.tokenOption = tokenOption;
        }

        /// <summary>
        /// 驗證
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidateCredentials(string account, string password)
        {
            if (authorizationOption.IsUseActiveDirectory)
            {
                var contextType = (ContextType)authorizationOption.ActiveDirectoryContextType;
                if (contextType == ContextType.Machine)
                    using (PrincipalContext principalContext = new PrincipalContext(contextType))
                        return principalContext.ValidateCredentials(account, password);

                if (contextType == ContextType.Domain)
                    using (PrincipalContext principalContext = new PrincipalContext(contextType, authorizationOption.ActiveDirectoryUserName, authorizationOption.ActiveDirectoryPassword))
                        return principalContext.ValidateCredentials(account, password);
            }

            return false;
        }

        /// <summary>
        /// 產生憑證
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public string GenerateToken(string account)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOption.SymmetricKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                tokenOption.AllowedIssuer,
                tokenOption.AllowedAudience,
                new Claim[]
                {
                    new Claim(ClaimTypes.Name, account)
                },
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOption.SymmetricKey));

            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParams = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                RequireExpirationTime = true,

                ValidateAudience = true,
                ValidAudience = tokenOption.AllowedAudience,

                ValidateIssuer = true,
                ValidIssuer = tokenOption.AllowedIssuer,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricSecurityKey
            };
            SecurityToken validatedToken;

            var validateResult = tokenHandler.ValidateToken(token.Replace("bearer ", ""), validationParams, out validatedToken);
            return validateResult;
        }
    }
}