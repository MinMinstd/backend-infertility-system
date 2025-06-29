namespace infertility_system.Interfaces
{
    using infertility_system.Models;

    public interface IJwtService
    {
        string GenerateToken(User user);
    }
} 