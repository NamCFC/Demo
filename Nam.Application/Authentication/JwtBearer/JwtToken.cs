using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nam.Application.Users.Dto;
using Nam.Core.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Nam.Application.Authentication.JwtBearer
{
    public static class JwtToken
    {
        public static string GenerateJSONWebToken(UserInfoDto user, IConfiguration config)
        {
            var claims =new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("UserName", user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
