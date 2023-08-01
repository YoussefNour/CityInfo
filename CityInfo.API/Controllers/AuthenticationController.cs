using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public class AuthenticationRequestBody
        {
            public string? Username { get; set; }
            public string? Password { get; set; }
        }

        private class CityInfoUser
        {
            public int UserId { get; set; }
            public string Username { get; set; }
            public string FirsName { get; set; }
            public string LastName { get; set; }
            public string City { get; set; }

            public CityInfoUser(
                int userId,
                string username,
                string firsName,
                string lastName,
                string city
            )
            {
                UserId = userId;
                Username = username;
                FirsName = firsName;
                LastName = lastName;
                City = city;
            }
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody requestBody)
        {
            var user = ValidateUserCredential(requestBody.Username, requestBody.Password);
            if (user == null)
                return Unauthorized();
            // step two generate token
            var securityToken = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Authentication:secretForKey"])
            );
            var signingCredentials = new SigningCredentials(
                securityToken,
                SecurityAlgorithms.HmacSha256
            );
            var calimsForToken = new List<Claim>
            {
                new Claim("sub", $"{user.UserId}"),
                new Claim("given_name", user.FirsName),
                new Claim("family_name", user.LastName),
                new Claim("City", user.City),
            };
            var jwtToken = new JwtSecurityToken(
                issuer: _configuration["Authentication:issuer"],
                audience: _configuration["Authentication:audience"],
                claims: calimsForToken,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signingCredentials
            );
            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return Ok(tokenToReturn);
        }

        private CityInfoUser ValidateUserCredential(string? username, string? password)
        {
            return new CityInfoUser(1, "YoussefOssama", "Youssef", "Ossama", "Alexandria");
        }
    }
}
