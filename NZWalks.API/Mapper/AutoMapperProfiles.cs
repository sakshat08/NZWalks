using AutoMapper;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;

namespace NZWalks.API.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            this.CreateMap<Region, RegionDto>().ReverseMap();
            this.CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            this.CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            this.CreateMap<AddRegionRequestDto, RegionDto>().ReverseMap();
            this.CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
            this.CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            this.CreateMap<Walk, WalkDto>().ReverseMap();
        }
    }
}
