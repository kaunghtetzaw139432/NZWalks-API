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
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbcontext;
        public RegionsController(NZWalksDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var regionsDomain = dbcontext.Regions.ToList();
            var regionDto = new List<RegionDto>();
            foreach(var regionDomain in regionsDomain)
            {
                regionDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });

            }
            return Ok(regionDto);

        }
        [HttpGet]
        [Route("{id:Guid}")]
        // ၁။ int id နေရာမှာ Guid id လို့ ပြောင်းရပါမယ်
        public IActionResult GetById([FromRoute] Guid id)
        {
            // ၂။ Find(id) သို့မဟုတ် FirstOrDefault ကို သုံးနိုင်ပါတယ်
            var regionDomain = dbcontext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomain == null)
            {
                return NotFound();
            }
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDto);
        }
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
           
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            dbcontext.Regions.Add(regionDomainModel);
            dbcontext.SaveChanges();
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
          
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id },regionDto);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // ၁။ ပြင်မယ့် ID နဲ့ data ရှိမရှိ အရင်ရှာမယ်
            var regionDomainModel = dbcontext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // ၂။ ရှိရင် DTO ထဲက data အသစ်တွေကို Domain Model ထဲ ထည့်မယ် (Map DTO to Domain Model)
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            // ၃။ Database ထဲ သိမ်းမယ်
            dbcontext.SaveChanges();

            // ၄။ ပြင်ပြီးသား data ကို client ဆီ ပြန်ပို့ဖို့ DTO ပြန်ပြောင်းမယ်
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            // ၁။ ဖျက်မယ့် ID နဲ့ data ရှိမရှိ အရင်ရှာမယ်
            var regionDomainModel = dbcontext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // ၂။ Database ထဲကနေ ဖျက်မယ်
            dbcontext.Regions.Remove(regionDomainModel);
            dbcontext.SaveChanges();

            // ၃။ (Optional) ဖျက်လိုက်တဲ့ data ကို client ဆီ ပြန်ပြချင်ရင် DTO ပြောင်းပြီး ပြန်ပို့လို့ရပါတယ်
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }
    }
}
