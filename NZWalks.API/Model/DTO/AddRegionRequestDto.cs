using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Model.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3,ErrorMessage = "Code Must be 3 Letters")]
        [MaxLength(3, ErrorMessage = "Code Must be 3 Letters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name Can not be more than 100 character.")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
