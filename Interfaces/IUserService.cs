namespace infertility_system.Interfaces
{
    using infertility_system.Models;

    public interface IUserService
    {
        Task<User?> GetByEmailAsync(string email);
    }
} 