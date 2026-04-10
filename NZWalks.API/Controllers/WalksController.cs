using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public WalksController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // 1. GET ALL WALKS
        [HttpGet]
        public IActionResult GetAll()
        {
            var walksDomain = dbContext.Walks
                .Include(x => x.Difficulty)
                .Include(x => x.Region)
                .ToList();

            var walksDto = walksDomain.Select(walkDomain => new WalkDto
            {
                Id = walkDomain.Id,
                Name = walkDomain.Name,
                Description = walkDomain.Description,
                LengthInKm = walkDomain.LengthInKm,
                WalkImageUrl = walkDomain.WalkImageUrl,
                Difficulty = new DifficultyDto
                {
                    Id = walkDomain.Difficulty.Id,
                    Name = walkDomain.Difficulty.Name
                },
                Region = new RegionDto
                {
                    Id = walkDomain.Region.Id,
                    Code = walkDomain.Region.Code,
                    Name = walkDomain.Region.Name,
                    RegionImageUrl = walkDomain.Region.RegionImageUrl
                }
            }).ToList();

            return Ok(walksDto);
        }

        // 2. GET WALK BY ID
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var walkDomain = dbContext.Walks
                .Include(x => x.Difficulty)
                .Include(x => x.Region)
                .FirstOrDefault(x => x.Id == id);

            if (walkDomain == null) return NotFound();

            var walkDto = new WalkDto
            {
                Id = walkDomain.Id,
                Name = walkDomain.Name,
                Description = walkDomain.Description,
                LengthInKm = walkDomain.LengthInKm,
                WalkImageUrl = walkDomain.WalkImageUrl,
                Difficulty = new DifficultyDto { Id = walkDomain.Difficulty.Id, Name = walkDomain.Difficulty.Name },
                Region = new RegionDto { Id = walkDomain.Region.Id, Code = walkDomain.Region.Code, Name = walkDomain.Region.Name, RegionImageUrl = walkDomain.Region.RegionImageUrl }
            };

            return Ok(walkDto);
        }

        // 3. CREATE WALK
        [HttpPost]
        public IActionResult Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            var walkDomainModel = new Walk
            {
                Name = addWalkRequestDto.Name,
                Description = addWalkRequestDto.Description,
                LengthInKm = addWalkRequestDto.LengthInKm,
                WalkImageUrl = addWalkRequestDto.WalkImageUrl,
                DifficultyId = addWalkRequestDto.DifficultyId,
                RegionId = addWalkRequestDto.RegionId
            };

            dbContext.Walks.Add(walkDomainModel);
            dbContext.SaveChanges();

            // ဆွဲထုတ်လိုက်တဲ့ data မှာ Region/Difficulty တွေပါအောင် ပြန်ရှာရပါမယ်
            var createdWalk = dbContext.Walks
                .Include(x => x.Difficulty)
                .Include(x => x.Region)
                .FirstOrDefault(x => x.Id == walkDomainModel.Id);

            var walkDto = new WalkDto
            {
                Id = createdWalk.Id,
                Name = createdWalk.Name,
                Description = createdWalk.Description,
                LengthInKm = createdWalk.LengthInKm,
                WalkImageUrl = createdWalk.WalkImageUrl,
                Difficulty = new DifficultyDto { Id = createdWalk.Difficulty.Id, Name = createdWalk.Difficulty.Name },
                Region = new RegionDto { Id = createdWalk.Region.Id, Code = createdWalk.Region.Code, Name = createdWalk.Region.Name, RegionImageUrl = createdWalk.Region.RegionImageUrl }
            };

            return CreatedAtAction(nameof(GetById), new { id = walkDto.Id }, walkDto);
        }

       ှ
    }
}