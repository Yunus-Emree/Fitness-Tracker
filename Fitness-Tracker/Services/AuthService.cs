using Fitness_Tracker.Entities;
using Fitness_Tracker.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fitness_Tracker.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(AppUser user)
        {
            var jwtDefaults = _configuration.GetSection("JwtDefaults");
            var secretKey = jwtDefaults["secretKey"];

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddMinutes(Convert.ToDouble(jwtDefaults["expires"]))).ToUnixTimeSeconds().ToString())
            };

            JwtSecurityToken token = new JwtSecurityToken(issuer: jwtDefaults["ValidIssuer"], audience: jwtDefaults["ValidAudience"], claims: claims, notBefore: DateTime.Now.AddMinutes(Convert.ToDouble(jwtDefaults["expires"])), signingCredentials: signingCredentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }
        public ClaimsPrincipal ValidateToken(string token)
        {
            var jwtDefaults = _configuration.GetSection("JwtDefaults");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtDefaults["secretKey"]);

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtDefaults["ValidIssuer"],
                    ValidAudience = jwtDefaults["ValidAudience"],
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return claimsPrincipal;
            }
            catch
            {
                return null;
            }
        }
    }
}
