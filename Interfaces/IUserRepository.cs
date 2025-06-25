using infertility_system.Models;

namespace infertility_system.Interfaces
{
    public interface IUserRepository
    {
        Task<User> UpdateUser(int id, User user);
        Task<User> GetUserAfterLogin(int userId);
        Task<int> CountTotalAccounts();
        Task<int> CountDoctorsAccount();
        Task<int> CountCustomerAccount();
        Task<List<User>> GetAllUsersForManagement();
    }
}
