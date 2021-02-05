using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetClock.Application.Common.Interfaces.Identity;
using NetClock.Application.Common.Options;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Common.Services.Identity
{
    public class JwtSecurityTokenService : IJwtSecurityTokenService
    {
        private readonly JwtOptions _jwtOptions;

        public JwtSecurityTokenService(IOptions<JwtOptions> appSettings)
        {
            _jwtOptions = appSettings.Value;
        }

        public string CreateToken(ApplicationUser user, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.ValidAudience,
                Issuer = _jwtOptions.ValidIssuer,
                Subject = AddClaimsRoles(user, roles),
                Expires = DateTime.UtcNow.AddHours(_jwtOptions.ExpiryMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private static ClaimsIdentity AddClaimsRoles(ApplicationUser user, IEnumerable<string> roles)
        {
            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            foreach (var role in roles)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return claimsIdentity;
        }
    }
}
