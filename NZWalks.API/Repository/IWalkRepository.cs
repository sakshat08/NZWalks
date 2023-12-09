using NZWalks.API.Model.DTO;

namespace NZWalks.API.Repository
{
    public interface IWalkRepository
    {
        public Task<WalkDto> CreateWalk(AddWalkRequestDto walk);
    }
}
