using infertility_system.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using static System.Net.Mime.MediaTypeNames;

namespace infertility_system.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImageController(IWebHostEnvironment webHostEnvironment, IImageService imageService)
        {
            _webHostEnvironment = webHostEnvironment;
            _imageService = imageService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return BadRequest("Vui lòng chọn ảnh");

            var fileName = await _imageService.UploadImageAsync(imageFile);

            var imageUrl = $"{Request.Scheme}://{Request.Host}/images/{fileName}";

            return Ok(new { imageUrl });
        }

        [HttpGet("{imageName}")]
        public async Task<IActionResult> GetImage(string imageName)
        {
            var imageFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            if (!Directory.Exists(imageFolderPath))
            {
                throw new FileNotFoundException("Folder not found");
            }

            var filePath = Path.Combine(imageFolderPath, imageName);

            var imageByte = await _imageService.LoadImageAsync(filePath);
            var contentType = _imageService.GetContentType(filePath);
            return File(imageByte, contentType, Path.GetFileName(filePath));
        }
    }
}
