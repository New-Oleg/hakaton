using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using UniversitySchedule.Application.DTOs.Auth;
using UniversitySchedule.Application.Interfaces.Repositories;
using UniversitySchedule.Application.Interfaces.Services;
using UniversitySchedule.Domain.Entities;
using UniversitySchedule.Domain.Enums;

namespace UniversitySchedule.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository<User> _userRepository;

        public UserService(IUserRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterAsync(RegisterDto registerDto)
        {
            // Простейшая регистрация без валидации
            var existingUser = await _userRepository.GetByUsernameAsync(registerDto.Username);
            if (existingUser != null)
            {
                return false;
            }

            var user = new User
            {
                Username = registerDto.Username,
                // На практике используйте хэширование пароля
                PasswordHash = registerDto.Password,
                Role = registerDto.Role.ToLower() == "admin"
                    ? UserRole.Admin
                    : registerDto.Role.ToLower() == "teacher"
                        ? UserRole.Teacher
                        : UserRole.Student
            };

            await _userRepository.AddAsync(user);
            return true;
        }

        public string HashPassword(string password, out string salt)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(16);
            salt = Convert.ToBase64String(saltBytes);

            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password, saltBytes,
                KeyDerivationPrf.HMACSHA256,
                10000, 32));

            return hash;
        }

        public bool VerifyPassword(string password, string hash, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            var enteredHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password, saltBytes,
                KeyDerivationPrf.HMACSHA256,
                10000, 32));

            return hash == enteredHash;
        }
    }
}
