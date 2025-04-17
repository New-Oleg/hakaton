using System.Threading.Tasks;
using UniversitySchedule.Application.DTOs.Auth;
using UniversitySchedule.Application.Interfaces.Repositories;
using UniversitySchedule.Application.Interfaces.Services;
using UniversitySchedule.Domain.Entities;
using UniversitySchedule.Domain.Enums;

namespace UniversitySchedule.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
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
    }
}
