using AutoMapper;
using infertility_system.Dtos.BlogPost;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace infertility_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IImageService imageService;
        private readonly IMapper mapper;

        public BlogPostController(IBlogPostRepository blogPostRepository, ICustomerRepository customerRepository, IImageService imageService, IMapper mapper)
        {
            this.blogPostRepository = blogPostRepository;
            this.customerRepository = customerRepository;
            this.imageService = imageService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogPosts = await this.blogPostRepository.GetAllBlogPostsAsync();
            if (blogPosts == null || !blogPosts.Any())
            {
                return NotFound("No blog posts found.");
            }
            return Ok(blogPosts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogPostById(int id)
        {
            var blogPosts = await this.blogPostRepository.GetAllBlogPostsAsync();
            var blogPost = blogPosts.FirstOrDefault(x => x.BlogPostId == id);
            if (blogPost == null)
            {
                return NotFound("Blog post not found.");
            }

            try
            {
                var fileBytes = await imageService.GetImageAsync(blogPost.Image);

                var contentType = "image/jpeg"; // hoặc xác định theo đuôi file
                var fileName = Path.GetFileName(blogPost.Image);

                return File(fileBytes, contentType, fileName);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBlogPost(BlogPostDto blogPostDto)
        {

            var userId = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var customer = await this.customerRepository.GetCustomersAsync(userId);
            var blogPost = this.mapper.Map<BlogPost>(blogPostDto);
            if (blogPost == null)
            {
                return BadRequest("Blog post data is invalid.");
            }
            blogPost.Image = await this.imageService.UploadImageAsync(blogPostDto.ImageFile);
            blogPost.CustomerId = customer.CustomerId;
            blogPost.ManagerId = 1;

            var result = await this.blogPostRepository.CreateBlogPostAsync(blogPost);
            return result ? Ok("Blog post created successfully.") : BadRequest("Failed to create blog post.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogPost(int id, [FromBody] string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                return BadRequest("Status cannot be empty.");
            }
            var result = await this.blogPostRepository.UppdateBlogPostAsync(id, status);
            return result ? Ok("Blog post updated successfully.") : NotFound("Blog post not found.");
        }
    }
}
