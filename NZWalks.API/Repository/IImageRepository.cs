using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repository
{
    public interface IImageRepository
    {
        public Task<Image> UploadImage(Image image);
    }
}
