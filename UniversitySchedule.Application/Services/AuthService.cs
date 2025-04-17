using System.Threading.Tasks;
using UniversitySchedule.Application.DTOs.Auth;
using UniversitySchedule.Application.Interfaces.Repositories;
using UniversitySchedule.Application.Interfaces.Services;
using UniversitySchedule.Domain.Entities;
using UniversitySchedule.Domain.Enums;
using UniversitySchedule.Infrastructure.Auth;

namespace UniversitySchedule.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository<User> _userRepository;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IUserRepository<User> userRepository, JwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByUsernameAsync(loginDto.Username);
            if (user == null || user.PasswordHash != loginDto.Password)
                return null;

            return _jwtTokenGenerator.GenerateToken(user);
        }

        public async Task<bool> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(registerDto.Username);
            if (existingUser != null)
                return false;

            var user = new User
            {
                Username = registerDto.Username,
                PasswordHash = registerDto.Password, // ❗ В проде — хэшируй!
                Role = registerDto.Role.ToLower() switch
                {
                    "admin" => UserRole.Admin,
                    "teacher" => UserRole.Teacher,
                    _ => UserRole.Student
                }
            };

            await _userRepository.AddAsync(user);
            return true;
        }
    }
}
