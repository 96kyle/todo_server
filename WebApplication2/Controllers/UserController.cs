using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Databases;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public string Login([FromBody] UserCreateModel model)
        {
            var idResult = DatabaseContext.Users.FirstOrDefault(p => p.Id == model.Id);

            if (idResult == null)
            {
                return "";
            }
            else if (!BCrypt.Net.BCrypt.Verify(model.Password, idResult.Password))
            {
                return "";
            }
            else
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Convert.FromBase64String(Startup.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, idResult.Index.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            }
        }

        [HttpPost("")]
        public string Register([FromBody] UserCreateModel model)
        {
            if (DatabaseContext.Users.FirstOrDefault(p => p.Id == model.Id) != null)
            {
                return "";
            }

            var result = DatabaseContext.Users.Add(new User
            {
                Id = model.Id,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Name = model.Name,
            });

            DatabaseContext.SaveChanges();


            return "success";
        }
    }

    public class UserCreateModel
    {
        public string Id { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }
    }

    public class LoginRequest
    {
        public string Token { get; set; }
        public int Id { get; set; }
    }
}