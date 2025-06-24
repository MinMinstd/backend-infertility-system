using AutoMapper;
using infertility_system.Dtos.User;
using infertility_system.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace infertility_system.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("GetUserAfterLogin")]
        public async Task<IActionResult> GetUser()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _userRepository.GetUserAfterLogin(userId);
            if (user == null)
            {
                return NotFound();
            }
            var userDto = _mapper.Map<UserRespondDto>(user);
            return Ok(userDto);
        }
    }
}
