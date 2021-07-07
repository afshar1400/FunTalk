using FunTalk.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FunTalk.Infrastructure.Identity
{
    public interface IJwtManager
    {
        Task<string> GenerateToken(string userId);

    }
    public class JwtManager : IJwtManager
    {

        private readonly JwtConfig jwtConfig;
        private readonly UserManager<AppUser> userManager;
        private readonly byte[] _secret;

        public JwtManager(
            JwtConfig jwtConfig,
            UserManager<AppUser> userManager)
        {
            this.jwtConfig = jwtConfig;
            this.userManager = userManager;
            _secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);
        }
        //generate token for user id
        public async Task<string> GenerateToken(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return null;
            //making all the claims for 
            var roles = await userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier,userId),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
            foreach (var userRole in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }


            // creating token 
            var tokenSecurity = new JwtSecurityToken(
                issuer: jwtConfig.Issuer,
                audience: jwtConfig.Audience,
                expires: DateTime.Now.AddDays(jwtConfig.AccessTokenExpiry),
                claims: authClaims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature)
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenSecurity);

            return token;
        }

    }
}

