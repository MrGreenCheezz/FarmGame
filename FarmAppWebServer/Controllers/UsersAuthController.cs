using FarmAppWebServer.Contexts;
using FarmAppWebServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;


namespace FarmAppWebServer.Controllers
{
    [ApiController]
    [Route("auth/")]
    public class UsersAuthController : ControllerBase
    {
        private readonly FarmAppDbContext _context;
        private readonly IConfiguration _configuration;

        public UsersAuthController(FarmAppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private string GenerateJwtToken(UserRegistrationInfo user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Secrets")["JwtSecret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "farmserver",
                audience: "farmuser",
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //Not secure but using https and storing hashed password with salt in DB way more complicated than its needed for small app
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(string username, string password, string email)
        {
            if (await _context.UserRegistrationInfos.AnyAsync(user => user.Name == username || user.Email == email))
            {
                return BadRequest("Username or email already registered!");
            }

            var newUser = DataFactory.CreateUserRegistration(email, username, password);
            await _context.UserRegistrationInfos.AddAsync(newUser);
            await _context.SaveChangesAsync();

            var registeredUserDefaultData = DataFactory.CreatePlayerData(newUser.Id);

            await _context.PlayerDataValues.AddAsync(registeredUserDefaultData);
            await _context.SaveChangesAsync();

            PlayersPlantsInstance[] plantsArray = new PlayersPlantsInstance[9];
            for(int i = 0; i < 9; i++)
            {
                var tmpPlant = DataFactory.CreatePlayerPlantInstace(newUser.Id, i);
                plantsArray[i] = tmpPlant;
            }
            await _context.PlayersPlantsInstanceValues.AddRangeAsync(plantsArray);
            await _context.SaveChangesAsync();

            return Ok($"New user with name {username} registered!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.UserRegistrationInfos.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || user.PasswordEncrypted != password)
            {
                return Unauthorized("Invalid email or password.");
            }

            var token = GenerateJwtToken(user);
            var tmpUserData = await _context.PlayerDataValues.FirstOrDefaultAsync(u => u.PlayerId == user.Id);
            if(tmpUserData != null)
            tmpUserData.LastToken = token;
            await _context.SaveChangesAsync();

            return Ok(new { Token = token });
        }

        [HttpGet("test")]
        [Authorize]
        public async Task<IActionResult> Test()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            return Ok(token);
        }
        
        
    }
}
