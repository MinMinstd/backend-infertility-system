﻿namespace infertility_system.Service
{
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

    public class AuthService : IAuthService
    {
        private readonly AppDbContext context;
        private readonly IConfiguration configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<string?> AuthenticateUserAsync(LoginRequestDto loginRequest)
        {
            var user = await this.context.Users
                .FirstOrDefaultAsync(u => (u.Email == loginRequest.Username || u.Phone == loginRequest.Username) && u.IsActive == true);
            if (user == null || !this.VerifyPasswordHash(loginRequest.Password, user.PasswordHash, user.PasswordSalt))
            {
                return null; // User not found
            }

            if (user.LastActiveAt < DateOnly.FromDateTime(DateTime.UtcNow))
            {
                user.TotalActiveDays += 1;
                user.LastActiveAt = DateOnly.FromDateTime(DateTime.UtcNow);
                this.context.Users.Update(user);
                await this.context.SaveChangesAsync();
            }

            return this.GenerateJwtToken(user);
        }

        public async Task<User?> RegisterUserAsync(RegisterRequestDto user)
        {
            var existingUser = await this.context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email || u.Phone == user.Phone);
            if (existingUser != null)
            {
                return null; // User already exists
            }

            this.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var newUser = new User
            {
                Email = user.Email,
                Phone = user.Phone,
                PasswordHash = passwordHash, // In a real application, ensure to hash the password
                PasswordSalt = passwordSalt, // Store the salt for password verification
                Role = "Customer", // Default role, can be changed based on requirements
                CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
                TotalActiveDays = 1,
                LastActiveAt = DateOnly.FromDateTime(DateTime.UtcNow),
                IsActive = false, // Default to false, can be activated later
                TokenConfirmation = Guid.NewGuid().ToString(), // Generate a unique token for email confirmation
            };
            this.context.Users.Add(newUser);
            await this.context.SaveChangesAsync();

            var newCustomer = new Customer
            {
                UserId = newUser.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                Gender = user.Gender,
                Birthday = user.Birthday,
                Address = user.Address,
            };
            this.context.Customers.Add(newCustomer);
            await this.context.SaveChangesAsync();

            return newUser; // Return the newly created user
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(this.configuration["JwtSettings:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.Phone),
                new Claim(ClaimTypes.Role, user.Role),
                }),
                Issuer = this.configuration["JwtSettings:Issuer"],
                Audience = this.configuration["JwtSettings:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(this.configuration["JwtSettings:ExpirationMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> RegisterDoctorAsync(RegisterRequestFromManagernDto userDto)
        {
            if (userDto == null)
            {
                return false;
            }

            this.CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var newUser = new User
            {
                Email = userDto.Email,
                Phone = userDto.Phone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = "Doctor",
                IsActive = true,
                TotalActiveDays = 1,
                LastActiveAt = DateOnly.FromDateTime(DateTime.UtcNow),
            };

            this.context.Users.Add(newUser);
            await this.context.SaveChangesAsync();

            var newDoctor = new Doctor
            {
                UserId = newUser.UserId,
                FullName = userDto.FullName,
                Email = userDto.Email,
                Phone = userDto.Phone,
                Experience = userDto.Experience,
                ServiceDBId = userDto.ServiceDBId,
            };
            this.context.Doctors.Add(newDoctor);
            await this.context.SaveChangesAsync();

            var newDoctorDegree = new DoctorDegree
            {
                DegreeName = userDto.DegreeName,
                DoctorId = newDoctor.DoctorId,
                GraduationYear = userDto.GraduationYear,
                Description = userDto.Description,
            };
            this.context.DoctorDegrees.Add(newDoctorDegree);
            await this.context.SaveChangesAsync();

            return true; // Trả về true nếu đăng ký thành công
        }

        public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto dto)
        {
            var user = this.context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                return await Task.FromResult(false);
            }

            if (!this.VerifyPasswordHash(dto.CurrentPassword, user.PasswordHash, user.PasswordSalt))
            {
                return await Task.FromResult(false);
            }

            if (dto.NewPassword != dto.ConfirmPassword)
            {
                return await Task.FromResult(false);
            }

            this.CreatePasswordHash(dto.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            this.context.Users.Update(user);
            return await this.context.SaveChangesAsync().ContinueWith(t => t.Result > 0);
        }

        public async Task<bool> ConfirmEmailAsync(string token)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.TokenConfirmation == token);
            if (user == null || user.IsActive == true)
            {
                return false; // Token không hợp lệ hoặc đã active
            }

            user.IsActive = true;
            user.TokenConfirmation = null; // Xoá token sau khi xác nhận
            this.context.Users.Update(user);
            await this.context.SaveChangesAsync();
            return true;
        }
    }
}
