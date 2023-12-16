using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Model.Domain
{
    public class Image
    {
        public Guid Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public string FileName { get; set; }

        public string? Description { get; set; }

        public string Extension { get; set; }

        public long FileSize { get; set; }

        public string FilePath { get; set; }
    }
}
