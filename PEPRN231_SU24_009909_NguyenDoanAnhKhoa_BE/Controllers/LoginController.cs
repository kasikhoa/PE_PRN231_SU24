using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PEPRN231_SU24_009909_NguyenDoanAnhKhoa_BE.Requests;
using Repository.Models;
using Repository.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PEPRN231_SU24_009909_NguyenDoanAnhKhoa_BE.Controllers
{
    public class LoginController : ApiControllerBase
    {
        private readonly PremierLeagueAccountService _premierLeagueAccount;
        private readonly IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _premierLeagueAccount = new PremierLeagueAccountService();
            _config = config;

        }
        [HttpPost]
        public IActionResult Login([FromBody] LoginModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            IActionResult response = Unauthorized();
            var user = _premierLeagueAccount.GetAll()
                .Where(x => x.EmailAddress == request.Email).FirstOrDefault();

            if (user != null && user.Password == request.Password)
            {
                var tokenString = GenerateJWT(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJWT(PremierLeagueAccount userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              new Claim[]
              {
                  new(ClaimTypes.Email, userInfo.EmailAddress),
                  new(ClaimTypes.Role, userInfo.Role.ToString()),
                  new("userId", userInfo.AccId.ToString()),
              },
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials
              );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
