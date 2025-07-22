namespace infertility_system.Interfaces
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(IFormFile imageFile);

        Task<byte[]> LoadImageAsync(string imagePath);

        string GetContentType(string fileName);
    }
}
