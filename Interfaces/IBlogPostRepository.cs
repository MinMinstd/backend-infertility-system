using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IBlogPostRepository
    {
        Task<List<BlogPost>> GetAllBlogPostsAsync();
        Task<bool> CreateBlogPostAsync(BlogPost blogPost);

        Task<bool> UppdateBlogPostAsync(int id, string status);

        Task<BlogPost?> GetBlogPostByIdAsync(int id);
    }
}
