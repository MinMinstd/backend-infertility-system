namespace infertility_system.Interfaces
{
    public interface IImageService
    {
        Task UploadImageAsync(IFormFile imageFile);

        Task<string> GetImageUrlAsync(string fileName);

    }
}
