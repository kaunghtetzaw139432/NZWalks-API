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
    public class DifficultiesController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public DifficultiesController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET ALL: api/difficulties
        [HttpGet]
        public IActionResult GetAll()
        {
            // 1. Get Data From Database (Domain Models)
            var difficultiesDomain = dbContext.Difficulties.ToList();

            // 2. Map Domain Models to DTOs
            var difficultyDto = new List<DifficultyDto>();
            foreach (var difficultyDomain in difficultiesDomain)
            {
                difficultyDto.Add(new DifficultyDto()
                {
                    Id = difficultyDomain.Id,
                    Name = difficultyDomain.Name
                });
            }

            // 3. Return DTOs
            return Ok(difficultyDto);
        }

        // GET BY ID: api/difficulties/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            // 1. Get Difficulty Domain Model From Database
            var difficultyDomain = dbContext.Difficulties.FirstOrDefault(x => x.Id == id);

            if (difficultyDomain == null)
            {
                return NotFound();
            }

            // 2. Map Domain Model to DTO
            var difficultyDto = new DifficultyDto
            {
                Id = difficultyDomain.Id,
                Name = difficultyDomain.Name
            };

            return Ok(difficultyDto);
        }

        // POST: api/difficulties
        [HttpPost]
        public IActionResult Create([FromBody] AddDifficultyRequestDto addDifficultyRequestDto)
        {
            // 1. Map DTO to Domain Model
            var difficultyDomainModel = new Difficulty
            {
                Name = addDifficultyRequestDto.Name
            };

            // 2. Use Domain Model to create Difficulty
            dbContext.Difficulties.Add(difficultyDomainModel);
            dbContext.SaveChanges();

            // 3. Map Domain Model back to DTO
            var difficultyDto = new DifficultyDto
            {
                Id = difficultyDomainModel.Id,
                Name = difficultyDomainModel.Name
            };

            return CreatedAtAction(nameof(GetById), new { id = difficultyDto.Id }, difficultyDto);
        }

        // PUT: api/difficulties/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateDifficultyRequestDto updateDifficultyRequestDto)
        {
            // 1. Check if the difficulty exists
            var difficultyDomainModel = dbContext.Difficulties.FirstOrDefault(x => x.Id == id);

            if (difficultyDomainModel == null)
            {
                return NotFound();
            }

            // 2. Map DTO to Domain Model
            difficultyDomainModel.Name = updateDifficultyRequestDto.Name;

            // 3. Save Changes
            dbContext.SaveChanges();

            // 4. Map Domain Model back to DTO
            var difficultyDto = new DifficultyDto
            {
                Id = difficultyDomainModel.Id,
                Name = difficultyDomainModel.Name
            };

            return Ok(difficultyDto);
        }

        // DELETE: api/difficulties/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            // 1. Find the difficulty
            var difficultyDomainModel = dbContext.Difficulties.FirstOrDefault(x => x.Id == id);

            if (difficultyDomainModel == null)
            {
                return NotFound();
            }

            // 2. Delete the difficulty
            dbContext.Difficulties.Remove(difficultyDomainModel);
            dbContext.SaveChanges();

            // 3. Map back to DTO
            var difficultyDto = new DifficultyDto
            {
                Id = difficultyDomainModel.Id,
                Name = difficultyDomainModel.Name
            };

            return Ok(difficultyDto);
        }
    }
}