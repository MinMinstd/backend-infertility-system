namespace infertility_system.Interfaces
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(IFormFile imageFile);

        Task<byte[]> GetImageAsync(string imagePath);
    }
}
