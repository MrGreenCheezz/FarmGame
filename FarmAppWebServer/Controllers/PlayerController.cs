using FarmAppWebServer.Contexts;
using FarmAppWebServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FarmAppWebServer.Controllers
{
    [ApiController]
    [Route("player/")]
    public class PlayerController : ControllerBase
    {

        private readonly FarmAppDbContext _context;
        private readonly IConfiguration _configuration;

        public PlayerController(FarmAppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        [HttpGet("playerData")]
        [Authorize]
        public async Task<IActionResult> GetPlayersData()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userdata = _context.PlayerDataValues.FirstOrDefault(user => user.LastToken == token);
            if(userdata == null)
            {
                return NotFound("No users found");
            }
            return Ok(userdata);
        }
    }
}
