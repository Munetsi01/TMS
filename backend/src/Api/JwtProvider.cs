using Core;
using Core.Abstractions;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api
{
    public sealed class JwtProvider : IJwtProvider<User>
    {
        public async Task<(string, DateTime)> GenereteAsync(User user)
        {
            int expireIn = 60 * 60;
            var tokenExpiryDate = DateTime.Now.AddSeconds(expireIn);
            IdentityOptions options = new IdentityOptions();
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("ExpireIn",  expireIn.ToString()),
                new Claim("http://schemas.microsoft.com/identity/claims/objectidentifier", user.Id.ToString()),
                new Claim(options.ClaimsIdentity.UserIdClaimType, user.Id.ToString()),
                new Claim(options.ClaimsIdentity.UserNameClaimType, user.Username),
            };

            var userRoles = new List<string>
            {
                user.Role.ToString(),//0 or 1
            };
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var jwtSettings = AppConfig.GetSettings<JwtSettings>();
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwtSettings.Issuer,
                jwtSettings.Audience,
                claims,
                null,
                tokenExpiryDate,
                signingCredentials);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return await System.Threading.Tasks.Task.FromResult((tokenValue, tokenExpiryDate));
        }
    }
}
