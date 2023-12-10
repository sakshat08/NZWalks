using NZWalks.API.Model.DTO;

namespace NZWalks.API.Repository
{
    public interface IWalkRepository
    {
        public Task<WalkDto> CreateWalk(AddWalkRequestDto walk);

        public Task<List<WalkDto>> GetAllWalks();

        public Task<WalkDto?> GetWalkById(Guid id);

        public Task<WalkDto?> UpdateWalk(Guid id, UpdateWalkRequestDto walk);

        public Task<WalkDto?> DeleteWalk(Guid id);
    }
}
