using Authorization.Extensibility.LoginModels;
using Authorization.Extensibility.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Security = DocumentDatabase.Extensibility.DTOs.Security;

namespace DataStorageApplication.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly Security secret;
        private readonly IAuthorizationProvider authorizationProvider;

        public AuthorizationController(IOptions<Security> secretKey, IAuthorizationProvider authorizationProvider)
        {
            secret = secretKey.Value;
            this.authorizationProvider = authorizationProvider;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]UserDTO user)
        {
            if (user == null)
            {
                return BadRequest("Invalid request");
            }

            if (authorizationProvider.CheckUserCredentials(user))
            {
                SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret.SecurityKey));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:59555",
                    audience: "http://localhost:59555",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost, Route("register")]
        public IActionResult Register([FromBody]UserDTO user)
        {
            if (user == null)
            {
                return BadRequest("Invalid request");
            }
      
            else
            {
                authorizationProvider.Create(user);
                return Ok();
            }
        }
    }
}