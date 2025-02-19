using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using CarparkInfoAPI.Data;
using CarparkInfoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarparkInfoAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly CarparkDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(CarparkDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token.  
        /// </summary>
        /// <param name="loginUser">The login credentials.</param>
        /// <returns>
        /// - **200 OK**: Returns a JWT token upon successful authentication.  
        /// - **400 Bad Request**: If the username or password is missing.  
        /// - **401 Unauthorized**: If the credentials are invalid.  
        /// </returns>
        /// <remarks>
        /// **Request Body Example:**  
        /// ```json
        /// {
        ///   "username": "test",
        ///   "password": "password"
        /// }
        /// ```  
        /// **Response Example:**  
        /// ```json
        /// {
        ///   "Token": "your_jwt_token_here"
        /// }
        /// ```  
        /// **How to Use the Token:**  
        /// - Copy the `Token` value from the response.  
        /// - In Swagger or API requests, enter it as `Bearer {token}` in the `Authorization` header.  
        /// ```
        /// Authorization: Bearer your_jwt_token_here
        /// ```  
        /// </remarks>
        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginUser)
        {
            if (string.IsNullOrWhiteSpace(loginUser.username) || string.IsNullOrWhiteSpace(loginUser.password))
                return BadRequest("Username and password are required.");

            var user = _context.Users.FirstOrDefault(u => u.username == loginUser.username.ToLower());

            if (user == null || user.password != loginUser.password)
                return Unauthorized("Invalid username or password");

            var token = GenerateJwtToken(user);

            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.user_id.ToString()),
                new Claim(ClaimTypes.Name, user.username)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
