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
    }
}
