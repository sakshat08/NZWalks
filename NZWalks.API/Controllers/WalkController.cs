using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repository;
using NZWalks.API.Validation;

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
        [CustomValidator]
        [Authorize(Roles = "Writer")]
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
        [Authorize(Roles = "Reader")]

        public async Task<IActionResult> GetAllWalks(
            [FromQuery] string? filterOn,
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy,
            [FromQuery] bool isAscending,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            var walkDtoList = await this.walkRepository.GetAllWalks(isAscending, filterOn, filterQuery, sortBy, pageNumber, pageSize);

            return Ok(walkDtoList);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
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
        [CustomValidator]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {

            if (updateWalkRequestDto == null)
            {
                return NotFound("You have added Empty Update Model");
            }

            var walkDto = await this.walkRepository.UpdateWalk(id, updateWalkRequestDto);

            if (walkDto == null)
            {
                return NotFound($"No Walk Founf associated to {id}");
            }

            return Ok(walkDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
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
