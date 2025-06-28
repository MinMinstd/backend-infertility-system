namespace infertility_system.Interfaces
{
    using infertility_system.Dtos.User;
    using infertility_system.Models;

    public interface ICustomerRepository
    {
        Task<Customer> GetCustomersAsync(int userId);

        Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto dto);

        Task<bool> CheckCustomerExistsAsync(int userId);

        Task<bool> CheckExistsByUserId(int id); // This method seems to be duplicated, consider removing one

        Task<Customer> UpdateCutomerAsync(int userId, Customer customer);

        Task<List<Doctor>> GetListDoctorsAsync();

        Task<Doctor> GetDoctorDetailAsync(int doctorId);
    }
}
