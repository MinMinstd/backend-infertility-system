using infertility_system.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace infertility_system.Service
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<byte[]> LoadImageAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Image not found", filePath);

            }
            return await File.ReadAllBytesAsync(filePath);
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

            // ✅ Resize và giảm chất lượng ảnh bằng ImageSharp
            using (var image = await Image.LoadAsync(imageFile.OpenReadStream()))
            {
                // Tuỳ chỉnh kích thước tối đa, ví dụ: 1024x1024
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(1024, 1024),
                    Mode = ResizeMode.Max
                }));

                // Giảm chất lượng JPEG (75%)
                var encoder = new JpegEncoder
                {
                    Quality = 75 // bạn có thể chỉnh từ 50–90 tuỳ nhu cầu
                };

                await image.SaveAsync(filePath, encoder);
            }

            return fileName;
        }

        public string GetContentType(string fileName)
        {
            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            return ext switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream"
            };
        }
    }
}
