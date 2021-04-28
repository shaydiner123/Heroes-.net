using Md_exercise.Core.Domain;
using Md_exercise.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Md_exercise.Core.Services.TokenGenerator
{

    public class TokenGenerator : ITokenGenerator
    {
        private IConfiguration configuration;

        public TokenGenerator(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Token GenerateToken(ApplicationUser user)
        {
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, user.Id), new Claim(ClaimTypes.Name, user.UserName) };
            byte[] secret = Encoding.ASCII.GetBytes(configuration["Jwt:secret"]);
            var key = new SymmetricSecurityKey(secret);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(double.Parse(configuration["Jwt:lifeTimeInHours"])),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return new Token
            {
                TokenValue = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresIn = token.ValidTo.ToLocalTime()
            };
        }
    }
}
