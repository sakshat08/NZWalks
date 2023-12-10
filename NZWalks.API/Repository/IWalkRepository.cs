using NZWalks.API.Model.DTO;

namespace NZWalks.API.Repository
{
    public interface IWalkRepository
    {
        public Task<WalkDto> CreateWalk(AddWalkRequestDto walk);

        public Task<List<WalkDto>> GetAllWalks(bool isAscending,
                                               string? filterOn = null,
                                               string? filterQuery = null,
                                               string? sortBy = null,
                                               int pageNumber = 1,
                                               int pageSize = 5);

        public Task<WalkDto?> GetWalkById(Guid id);

        public Task<WalkDto?> UpdateWalk(Guid id, UpdateWalkRequestDto walk);

        public Task<WalkDto?> DeleteWalk(Guid id);
    }
}
