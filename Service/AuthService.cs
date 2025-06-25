using infertility_system.Data;
using infertility_system.Dtos.Admin;
using infertility_system.Dtos.User;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace infertility_system.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string?> AuthenticateUserAsync(LoginRequestDto loginRequest)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginRequest.Username || u.Phone == loginRequest.Username);
            if (user == null || !VerifyPasswordHash(loginRequest.Password, user.PasswordHash, user.PasswordSalt))
            {
                return null; // User not found
            }

            if (user.LastActiveAt < DateOnly.FromDateTime(DateTime.UtcNow))
            {
                user.TotalActiveDays += 1;
                user.LastActiveAt = DateOnly.FromDateTime(DateTime.UtcNow);
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            
            return GenerateJwtToken(user);
        }

        public async Task<User?> RegisterUserAsync(RegisterRequestDto user)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email || u.Phone == user.Phone);
            if (existingUser != null)
            {
                return null; // User already exists
            }
            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var newUser = new User
            {
                Email = user.Email,
                Phone = user.Phone,
                PasswordHash = passwordHash, // In a real application, ensure to hash the password
                PasswordSalt = passwordSalt, // Store the salt for password verification
                Role = "Customer", // Default role, can be changed based on requirements
                CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            var newCustomer = new Customer
            {
                UserId = newUser.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                Gender = user.Gender,
                Birthday = user.Birthday,
                Address = user.Address
            };
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();

            return newUser; // Return the newly created user
        }

        private void CreatePasswordHash(string password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(PasswordHash);
            }
        }
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.Phone),
                new Claim(ClaimTypes.Role, user.Role)
            }),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtSettings:ExpirationMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<String?> RegisterDoctorAndManagerAsync(RegisterRequestFromAdminDto userDto)
        {
            if (userDto.Role != "Manager" && userDto.Role != "Doctor")
                return "Admin chỉ có thể tạo Manager hoặc Doctor!";

            CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var newUser = new User
            {
                Email = userDto.Email,
                Phone = userDto.Phone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = userDto.Role
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            if (userDto.Role == "Doctor")
            {
                var newDoctor = new Doctor
                {
                    UserId = newUser.UserId,
                    FullName = userDto.FullName,
                    Email = userDto.Email,
                    Phone = userDto.Phone,
                    Experience = userDto.Experience
                };
                _context.Doctors.Add(newDoctor);
            }
            else if (userDto.Role == "Manager")
            {
                var newManager = new Manager
                {
                    UserId = newUser.UserId,
                    FullName = userDto.FullName,
                    Email = userDto.Email,
                    Phone = userDto.Phone,
                    Address = userDto.Address
                };
                _context.Managers.Add(newManager);
            }

            await _context.SaveChangesAsync();
            return "Tài khoản đã được tạo thành công!";
        }

        public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto dto)
        {

            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                return await Task.FromResult(false);
            }
            if (!VerifyPasswordHash(dto.CurrentPassword, user.PasswordHash, user.PasswordSalt))
            {
                return await Task.FromResult(false);
            }
            if (dto.NewPassword != dto.ConfirmPassword)
            {
                return await Task.FromResult(false);
            }

            CreatePasswordHash(dto.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _context.Users.Update(user);
            return await _context.SaveChangesAsync().ContinueWith(t => t.Result > 0);
        }
    }
}
