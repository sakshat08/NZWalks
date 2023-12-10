using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;

        public WalkController(IWalkRepository walkRepository) 
        {
            this.walkRepository = walkRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkRequestDto addWalksRequestDto)
        {
            if (addWalksRequestDto == null)
            {
                return BadRequest("Walk Details are Empty");
            }

            var walkDto = await this.walkRepository.CreateWalk(addWalksRequestDto);

            return Ok(walkDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var walkDtoList = await this.walkRepository.GetAllWalks();

            return Ok(walkDtoList);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walkDto = await this.walkRepository.GetWalkById(id);

            if (walkDto == null)
            {
                return NotFound($"No Walk Founf associated to {id}");
            }

            return Ok(walkDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            if (updateWalkRequestDto == null)
            {
                return NotFound("You have added Empty Update Model");
            }

            var walkDto = await this.walkRepository.UpdateWalk(id, updateWalkRequestDto);

            if(walkDto == null)
            {
                return NotFound($"No Walk Founf associated to {id}");
            }

            return Ok(walkDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            var walkDto = await this.walkRepository.DeleteWalk(id);

            if (walkDto == null)
            {
                return NotFound($"No Walk Founf associated to {id}");
            }

            return Ok(walkDto);
        }
    }
}
