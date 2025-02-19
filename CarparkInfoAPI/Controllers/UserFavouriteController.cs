using Microsoft.AspNetCore.Mvc;
using CarparkInfoAPI.Models;
using CarparkInfoAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CarparkInfoAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserFavouriteController : ControllerBase
    {
        private readonly CarparkDbContext _context;

        public UserFavouriteController(CarparkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a carpark to the user's favourites.  
        /// **Requires authentication.**
        /// </summary>
        /// <param name="carparkNo">The carpark number to favorite. E.g.: A1</param>
        /// <returns>
        /// - **200 OK**: Carpark successfully added to favourites.  
        /// - **401 Unauthorized**: User is not authenticated.  
        /// - **404 Not Found**: User or carpark does not exist.  
        /// - **409 Conflict**: Carpark is already in favourites.  
        /// </returns>
        /// <remarks>
        /// **Authorization:** Requires a valid JWT token in the `Authorization` header.  
        /// Example: `Authorization: Bearer {your_token}`
        /// </remarks>
        [Authorize]
        [HttpPost("add")]
        public IActionResult AddFavourite([FromQuery] string carparkNo)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized("Invalid token.");

            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized("Invalid user ID in token.");

            var user = _context.Users.FirstOrDefault(u => u.user_id == userId);
            if (user == null) return NotFound("User not found.");

            var carpark = _context.Carparks.FirstOrDefault(c => c.car_park_no == carparkNo);
            if (carpark == null) return NotFound("Carpark not found.");

            bool alreadyExists = _context.UserFavourites.Any(f => f.user_id == userId && f.car_park_no == carparkNo);
            if (alreadyExists) return Conflict("Carpark already added to favourites.");

            _context.UserFavourites.Add(new UserFavourite { user_id = userId, car_park_no = carparkNo });
            _context.SaveChanges();

            return Ok("Carpark added to favourites.");
        }
    }
}
