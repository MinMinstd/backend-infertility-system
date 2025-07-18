using infertility_system.Interfaces;

namespace infertility_system.Service
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<byte[]> GetImageAsync(string imagePath)
        {
            if (!File.Exists(imagePath))
                throw new FileNotFoundException("Image not found.", imagePath);

            return await File.ReadAllBytesAsync(imagePath);
        }

        public async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            var imageFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            if (!Directory.Exists(imageFolderPath))
            {
                Directory.CreateDirectory(imageFolderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(imageFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            return filePath;
        }


    }
}
