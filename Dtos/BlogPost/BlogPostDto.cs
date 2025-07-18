using Microsoft.AspNetCore.Http;

namespace infertility_system.Dtos.BlogPost
{
    public class BlogPostDto
    {
        public string? Title { get; set; }

        public string? Story { get; set; }

        public string? TreatmentType { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
