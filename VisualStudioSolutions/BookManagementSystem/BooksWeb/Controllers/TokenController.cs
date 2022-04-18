using BooksWeb.ViewModels;
using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BooksWeb.Controllers
{
    [ApiController]
    [Route("/api/token")]
    public class TokenController:Controller
    {
        IUserManager manager;
        IConfiguration _configuration;
        public TokenController(IUserManager manager,IConfiguration _configuration)
        {
            this.manager = manager;
            this._configuration = _configuration;
        }


        [HttpPost]
        public async Task<IActionResult> Token([FormBody]LoginUser credentials)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await manager.Login(credentials.Email, credentials.Password);

            if (user == null)
                return Unauthorized();

            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Name", user.Name),
                    new Claim("Email", user.Email)                    
           };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));

        } //A Web Api Authentication usign a Token


    }
}
