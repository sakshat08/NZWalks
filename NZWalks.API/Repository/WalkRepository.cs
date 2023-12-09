using AutoMapper;
using NZWalks.API.Database;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;

namespace NZWalks.API.Repository
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;
        private readonly IMapper mapper;

        public WalkRepository(NZWalksDbContext nZWalksDbContext, IMapper mapper)
        {
            this.mapper = mapper;
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<WalkDto> CreateWalk(AddWalkRequestDto walk)
        {
            // Dto to Domain mapping
            var walkDomainModel = this.mapper.Map<Walk>(walk);

            await this.nZWalksDbContext.Walks.AddAsync(walkDomainModel);

            this.nZWalksDbContext.SaveChanges();

            var walkDto = this.mapper.Map<WalkDto>(walkDomainModel);

            return walkDto;
        }
    }
}
