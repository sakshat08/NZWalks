using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repository;
using NZWalks.API.Validation;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;

        public RegionController(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllregion()
        {
            var regions = await this.regionRepository.GetAllRegion();
            return Ok(regions);
        }

        [HttpGet()]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {

            var region = await this.regionRepository.GetRegionById(id);

            if (region == null)
            {
                return NotFound("Region not Found");
            }

            return Ok(region);
        }

        [HttpPost]
        [CustomValidator]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            if (addRegionRequestDto == null)
            {
                return BadRequest();
            }

            var regionDto = await this.regionRepository.CreateRegion(addRegionRequestDto);

            return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        [CustomValidator]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {

            var regionDto = await this.regionRepository.UpdateRegion(id, updateRegionRequestDto);

            if (regionDto == null)
            {
                return NotFound("Region ID is Incorrect");
            }
            else
            {
                return Ok(regionDto);
            }
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var regionDto = await this.regionRepository.DeleteRegion(id);

            if (regionDto == null)
            {
                return NotFound("Region ID is Incorrect");
            }
            else
            {
                return Ok(regionDto);
            }
        }
    }
}
