using infertility_system.Data;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.EntityFrameworkCore;

namespace infertility_system.Repository
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly AppDbContext context;

        public BlogPostRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateBlogPostAsync(BlogPost blogPost)
        {
            if (blogPost == null)
            {
                return await Task.FromResult(false);
            }
            await this.context.BlogPosts.AddAsync(blogPost);
            await this.context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public Task<List<BlogPost>> GetAllBlogPostsAsync()
        {
            return this.context.BlogPosts.ToListAsync();
        }

        public async Task<bool> UppdateBlogPostAsync(int id, string status)
        {
            var existingBlogPost = await this.context.BlogPosts.FirstOrDefaultAsync(x => x.BlogPostId == id);
            if (existingBlogPost == null)
            {
                return await Task.FromResult(false);
            }

            existingBlogPost.Status = status;
            await this.context.SaveChangesAsync();
            return await Task.FromResult(true);
        }
    }
}
