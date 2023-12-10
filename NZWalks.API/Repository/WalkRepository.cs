using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<WalkDto?> DeleteWalk(Guid id)
        {
            var walkDomainModel = await this.nZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (walkDomainModel == null)
            {
                return null;
            }

            this.nZWalksDbContext.Remove(walkDomainModel);

            await this.nZWalksDbContext.SaveChangesAsync();

            var walkDtoModel = this.mapper.Map<WalkDto>(walkDomainModel);

            return walkDtoModel;
        }

        public async Task<List<WalkDto>> GetAllWalks(bool isAscending, string? filterOn = null, string? filterQuery = null, string? sortBy = null)
        {
            // Converting to Query Method
            var walks = this.nZWalksDbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            

            // Filtering 
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.InvariantCultureIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(sortBy)) 
            {
                if (sortBy.Equals("Name", StringComparison.InvariantCultureIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("LengthInKm", StringComparison.InvariantCultureIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            var walksDomainList = await walks.ToListAsync();

            var walkDtoList = this.mapper.Map<List<WalkDto>>(walksDomainList);

            return walkDtoList;
        }

        public async Task<WalkDto?> GetWalkById(Guid id)
        {
            var walkDomainModel = await this.nZWalksDbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);

            if (walkDomainModel == null)
            {
                return null;
            }

            // Domain to Dto mapping
            var walkDtoModel = this.mapper.Map<WalkDto>(walkDomainModel);

            return walkDtoModel;
        }

        public async Task<WalkDto?> UpdateWalk(Guid id, UpdateWalkRequestDto walk)
        {
            var existingWalkDomainModel = await this.nZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalkDomainModel == null)
            {
                return null;
            }

            existingWalkDomainModel.Name = walk.Name;
            existingWalkDomainModel.Description = walk.Description;
            existingWalkDomainModel.LengthInKm = walk.LengthInKm;
            existingWalkDomainModel.WalkImageUrl = walk.WalkImageUrl;
            existingWalkDomainModel.DifficultyId = walk.DifficultyId;
            existingWalkDomainModel.RegionId = walk.RegionId;

            await this.nZWalksDbContext.SaveChangesAsync();

            var walkDomainModel = await this.nZWalksDbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);


            var walkDtoModel = this.mapper.Map<WalkDto>(walkDomainModel);
            return walkDtoModel;
        }
    }
}
