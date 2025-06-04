using infertility_system.Dtos.Customer;
using infertility_system.Models;

namespace infertility_system.Mappers
{
    public static class CustomerMappers
    {
        public static CustomerDto ToCustomerDto(this Customer customer)
        {
            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                FullName = customer.FullName,
                Email = customer.Email,
                Phone = customer.Phone,
                Gender = customer.Gender,
                Birthday = customer.Birthday,
                Address = customer.Address
            };
        }
    }
}
