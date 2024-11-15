using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        string GenerateToken(string username, string secretKey, string issuer, string audience)
        { 
            var tokenHandler = new JwtSecurityTokenHandler(); 
            var key = Encoding.UTF8.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor { 
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }), 
                Expires = DateTime.UtcNow.AddHours(1), 
                Issuer = issuer, 
                Audience = audience, 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) }; 
            var token = tokenHandler.CreateToken(tokenDescriptor); 
            return tokenHandler.WriteToken(token); }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (model.Username == "user" && model.Password == "password") // Simplified for demonstration
            {
                var token = GenerateToken(model.Username, Common._secretKey, Common._issuer, Common._audience);
                return Ok(new { token });
            }
            return Unauthorized();
        }
    }
    

    public class LoginModel { public string Username { get; set; } public string Password { get; set; } }
}

