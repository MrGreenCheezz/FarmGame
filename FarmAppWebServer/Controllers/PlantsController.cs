using FarmAppWebServer.Contexts;
using FarmAppWebServer.Misc;
using FarmAppWebServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FarmAppWebServer.Controllers
{
    [ApiController]
    [Route("plants/")]
    public class PlantsController  : ControllerBase
    {
        

        private readonly FarmAppDbContext _context;
        private readonly DataCache _dataCache;

        public PlantsController(FarmAppDbContext context, DataCache cache)
        {
            _context = context;
            _dataCache = cache;
        }

        [HttpGet("allPlants")]
        [Authorize]
        public async Task<IActionResult> GetAllPlayersPlants()
        {
            var playerData = await GetPlayerDataFromToken(HttpContext);
            var plantTypes = _dataCache.GetPlantsTypes();
            if (playerData == null)
            {
                return NotFound("No data for player with that token!");
            }
            var plants = await _context.PlayersPlantsInstanceValues.Where(plant => plant.OwnerId == playerData.PlayerId).ToArrayAsync();

            
            foreach (var plant in plants)
            {
                plant.CurrentGrowState = GetPlantStageFromLastAction(plant);
                plant.LastClientInteraction = DateTime.Now;
            }
            await _context.SaveChangesAsync();
            return Ok(plants);
        }

        [HttpGet("plantsTypes")]
        public async Task<IActionResult> GetAllPlantsTypes()
        {
            var plants = _dataCache.GetPlantsTypes().ToList();
            if (plants.Count > 0)
            {
                return Ok(plants);
            }
            else
            {
                return BadRequest("No plants types");
            }
        }

        [HttpPost("buyPlant")]
        [Authorize]
        public async Task<IActionResult> BuyPlant(int potIndex, int plantType)
        {
            var playerData = await GetPlayerDataFromToken(HttpContext);
            if (playerData == null)
            {
                return NotFound("No data for player with that token!");
            }
            var price = _dataCache.GetPlantsTypes().FirstOrDefault(type => type.Id == plantType).StorePrice;
            if (playerData.Money < price)
            {
                return BadRequest("Not enough money");
            }
            var plant = _context.PlayersPlantsInstanceValues.FirstOrDefault(plant => plant.PotIndex == potIndex && plant.OwnerId == playerData.PlayerId);
            if(plant != null)
            {
                return BadRequest("Another plant is already planted");
            }
            playerData.Money -= price;
            var newPlant = DataFactory.CreatePlayerPlantInstace(playerData.PlayerId, potIndex, DateTime.Now, 0, 0, plantType);
            await _context.PlayersPlantsInstanceValues.AddAsync(newPlant);
            await _context.SaveChangesAsync();

            return Ok(newPlant);
        }

        [HttpPost("deletePlant")]
        [Authorize]
        public async Task<IActionResult> DeletePlant(int potIndex)
        {
            var playerData = await GetPlayerDataFromToken(HttpContext);
            if (playerData == null)
            {
                return NotFound("No data for player with that token!");
            }
            var plant = await _context.PlayersPlantsInstanceValues.FirstOrDefaultAsync(plant => plant.OwnerId == playerData.PlayerId && plant.PotIndex == potIndex);
            if (plant == null)
            {
                return BadRequest("No plant found!");
            }
            _context.PlayersPlantsInstanceValues.Remove(plant);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("harvestPlant")]
        [Authorize]
        public async Task<IActionResult> HarvestPlant(int potIndex)
        {
            var playerData = await GetPlayerDataFromToken(HttpContext);
            if (playerData == null)
            {
                return NotFound("No data for player with that token!");
            }

            var plant = await _context.PlayersPlantsInstanceValues.FirstOrDefaultAsync(plant => plant.OwnerId == playerData.PlayerId && plant.PotIndex == potIndex);
            if(plant == null)
            {
                return BadRequest("No plant found!");
            }
            var plantType = _dataCache.GetPlantsTypes().FirstOrDefault(plantType => plantType.Id == plant.PlantType);

            if(plant.CurrentGrowState < plantType.MaxGrowState && GetPlantStageFromLastAction(plant) < plantType.MaxGrowState)
            {
                return BadRequest($"Plant not ready current state-{plant.CurrentGrowState}, maxGrow-{plantType.MaxGrowState}, StateFromLogin-{GetPlantStageFromLastAction(plant)}");
            }

            plant.CurrentGrowState = 0;
            playerData.Money += plantType.HarvestedPrice;
            plant.LastClientInteraction = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok(plant);
        }

        private async Task<PlayerData> GetPlayerDataFromToken(HttpContext requestContext)
        {
            var token = requestContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var playerData = await _context.PlayerDataValues.FirstOrDefaultAsync(player => player.LastToken == token);
            return playerData;
        }

        private int GetPlantStageFromLastAction(PlayersPlantsInstance plant)
        {
            var plantTypes = _dataCache.GetPlantsTypes();
            var curPlantType = plantTypes.FirstOrDefault(pl => pl.Id == plant.PlantType);
            var secondsFromLastAction = (DateTime.Now - plant.LastClientInteraction).TotalSeconds;
            int totalStages = (int)secondsFromLastAction / curPlantType.SecondsToGrowOneState;
            var result = Math.Clamp(plant.CurrentGrowState + totalStages, 0, curPlantType.MaxGrowState);
            return result;
        }

    }
}

