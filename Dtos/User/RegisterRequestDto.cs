﻿namespace infertility_system.Dtos.User
{
    public class RegisterRequestDto
    {
        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Password { get; set; }

        public string Gender { get; set; }

        public DateOnly Birthday { get; set; }

        public string? Address { get; set; }
    }
}
