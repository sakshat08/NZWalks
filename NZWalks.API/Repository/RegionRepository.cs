using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Database;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;

namespace NZWalks.API.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;
        private readonly IMapper mapper;

        public RegionRepository(
            NZWalksDbContext nZWalksDbContext,
            IMapper mapper)
        {
            this.nZWalksDbContext = nZWalksDbContext;
            this.mapper = mapper;
        }

        public async Task<List<RegionDto>> GetAllRegion()
        {
            // Get the Domain model from Database
            var regionDomainList = await this.nZWalksDbContext.Regions.ToListAsync();

            // Domain to DTO mapping
            var regionDtoList = this.mapper.Map<List<RegionDto>>(regionDomainList);

            return regionDtoList;

        }

        public async Task<RegionDto?> GetRegionById(Guid id)
        {
            var regionDomainModel = await this.nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            
            if (regionDomainModel == null)
            {
                return null;
            }
            else
            {
                var regionDtoModel = this.mapper.Map<RegionDto>(regionDomainModel);

                return regionDtoModel;
            }
        }

        public async Task<RegionDto> CreateRegion(AddRegionRequestDto addRegionRequestDto)
        {
            // Request Dto to Domain mapping
            var regionDomainModel = this.mapper.Map<Region>(addRegionRequestDto);
            
            await this.nZWalksDbContext.Regions.AddAsync(regionDomainModel);
            this.nZWalksDbContext.SaveChanges();

            // Domain to Dto mapping
            var regionDtoModel = this.mapper.Map<RegionDto>(regionDomainModel);

            return regionDtoModel;
        }

        public async Task<RegionDto?> UpdateRegion(Guid id, UpdateRegionRequestDto region)
        {
            var existingregion = await this.nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingregion == null)
            {
                return null;
            }
            else
            {
                existingregion.Id = id;
                existingregion.Name = region.Name;
                existingregion.Code = region.Code;
                existingregion.RegionImageUrl = region.RegionImageUrl;

                await this.nZWalksDbContext.SaveChangesAsync();

                // Domain to Dto mapping

                var regionDtoModel = this.mapper.Map<RegionDto>(existingregion);

                return regionDtoModel; 
            }
        }

        public async Task<RegionDto?> DeleteRegion(Guid id)
        {
            var regionDomainModel = await this.nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return null;
            }
            else
            {
                this.nZWalksDbContext.Remove(regionDomainModel);
                this.nZWalksDbContext.SaveChanges();

                // Domain to Dto mapping
                var regionDto =  this.mapper.Map<RegionDto>(regionDomainModel);

                return regionDto;
            }
        }
    }
}
