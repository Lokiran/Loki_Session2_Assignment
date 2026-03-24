using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ProductInventoryManagement.Data;
using ProductInventoryManagement.Models;

namespace ProductInventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ProductDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(ProductDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto request)
        {
            var user = _context.UserAccounts
                .FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);

            if (user == null)
                return Unauthorized("Invalid Credentials");

            string token = GenerateToken(user);

            return Ok(new { token, role = user.Role });
        }


        private string GenerateToken(UserAccount user)
        {
            var jwtSettings = _config.GetSection("Jwt");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"])
            );

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}