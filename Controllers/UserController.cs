namespace infertility_system.Controllers
{
    using System.Security.Claims;
    using AutoMapper;
    using infertility_system.Dtos.User;
    using infertility_system.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet("GetUserAfterLogin")]
        public async Task<IActionResult> GetUser()
        {
            int userId = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await this.userRepository.GetUserAfterLogin(userId);
            if (user == null)
            {
                return this.NotFound();
            }

            var userDto = this.mapper.Map<UserRespondDto>(user);
            return this.Ok(userDto);
        }

        [HttpGet("CountTotalAccounts")]
        public async Task<IActionResult> CountTotalAccounts()
        {
            var totalAccounts = await this.userRepository.CountTotalAccounts();
            return this.Ok(totalAccounts);
        }

        [HttpGet("CountDoctorsAccount")]
        public async Task<IActionResult> CountDoctorsAccount()
        {
            var totalDoctors = await this.userRepository.CountDoctorsAccount();
            return this.Ok(totalDoctors);
        }

        [HttpGet("CountCustomerAccount")]
        public async Task<IActionResult> CountCustomerAccount()
        {
            var totalCustomers = await this.userRepository.CountCustomerAccount();
            return this.Ok(totalCustomers);
        }

        [HttpGet("GetAllUsersForManagement")]
        public async Task<IActionResult> GetAllUsersForManagement()
        {
            var users = await this.userRepository.GetAllUsersForManagement();
            if (users == null || !users.Any())
            {
                return this.NotFound();
            }

            var userDtos = this.mapper.Map<List<UserToManagementDto>>(users);
            return this.Ok(userDtos);
        }
    }
}
