using NZWalks.API.Model.DTO;

namespace NZWalks.API.Repository
{
    public interface IRegionRepository
    {
        public Task<List<RegionDto>> GetAllRegion();

        public Task<RegionDto?> GetRegionById(Guid Id);

        public Task<RegionDto> CreateRegion(AddRegionRequestDto region);

        public Task<RegionDto?> UpdateRegion(Guid id, UpdateRegionRequestDto region);

        public Task<RegionDto?> DeleteRegion(Guid RegionId);
    }
}
