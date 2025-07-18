namespace infertility_system.Controllers
{
    using AutoMapper;
    using infertility_system.Dtos.Admin;
    using infertility_system.Dtos.User;
    using infertility_system.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IAuthService authService;
        private readonly IMapper mapper;

        public UserController(IUserRepository userRepository, IMapper mapper, IAuthService authService)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.authService = authService;
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

        [HttpGet("statistics/total")]
        public async Task<IActionResult> CountTotalAccounts()
        {
            var totalAccounts = await this.userRepository.CountTotalAccounts();
            return this.Ok(totalAccounts);
        }

        [HttpGet("statistics/doctors")]
        public async Task<IActionResult> CountDoctorsAccount()
        {
            var totalDoctors = await this.userRepository.CountDoctorsAccount();
            return this.Ok(totalDoctors);
        }

        [HttpGet("statistics/customers")]
        public async Task<IActionResult> CountCustomerAccount()
        {
            var totalCustomers = await this.userRepository.CountCustomerAccount();
            return this.Ok(totalCustomers);
        }

        [HttpGet("statistics/new")]
        public async Task<IActionResult> CountNewAccount()
        {
            var totalNewAccounts = await this.userRepository.CountNewAccount();
            return this.Ok(totalNewAccounts);
        }

        [HttpGet]
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

        [HttpPost("doctor")]
        public async Task<IActionResult> CreateDoctorByManager([FromBody] RegisterRequestFromManagernDto userDto)
        {
            if (userDto == null)
            {
                return this.BadRequest("User data is required.");
            }

            var result = authService.RegisterDoctorAsync(userDto);
            return result.IsCompletedSuccessfully
                ? this.Ok("Doctor registered successfully.")
                : this.BadRequest("Failed to register doctor.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await this.userRepository.DeleteUser(id);
            return this.Ok("User deleted successfully.");
        }
    }
}
