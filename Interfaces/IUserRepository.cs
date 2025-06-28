namespace infertility_system.Interfaces
{
    using infertility_system.Models;

    public interface IUserRepository
    {
        Task<User> UpdateUser(int id, User user);

        Task<User> GetUserAfterLogin(int userId);

        Task<int> CountTotalAccounts();

        Task<int> CountDoctorsAccount();

        Task<int> CountCustomerAccount();

        Task<int> CountNewAccount();

        Task<List<User>> GetAllUsersForManagement();
    }
}
