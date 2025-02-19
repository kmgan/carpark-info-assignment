using Microsoft.AspNetCore.Mvc;
using CarparkInfoAPI.Models;
using CarparkInfoAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CarparkInfoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarparkController : ControllerBase
    {
        private readonly CarparkDbContext _context;

        public CarparkController(CarparkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of carparks filtered based on free parking availability, night parking, and gantry height.
        /// </summary>
        /// <param name="min_height">
        /// The minimum vehicle height (in meters) to filter by.  
        /// - If `min_height` is not provided, it defaults to `0m`.
        /// - If a carpark has `gantry_height` set to `0`, it means there is **no height restriction**.
        /// - Otherwise, only carparks where `gantry_height` is greater than `min_height` will be included.
        /// </param>
        /// <returns>A list of carparks that match the filter criteria.</returns>
        /// <response code="200">Returns the filtered carparks.</response>
        /// <response code="400">If the request parameters are invalid.</response>
        [HttpGet(Name = "Filter")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetFilteredCarparks([FromQuery] float? min_height)
        {
            var query = _context.Carparks.AsQueryable();

            query = query.Where(c => c.free_parking != "NO");
            query = query.Where(c => c.night_parking == true);
            query = query.Where(c => c.gantry_height == 0 || c.gantry_height > (min_height ?? 0));

            var result = query.ToList();
            return Ok(result);
        }
    }
}
