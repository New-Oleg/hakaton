using System.Threading.Tasks;
using UniversitySchedule.Application.DTOs.Auth;

namespace UniversitySchedule.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDto loginDto);
        Task<bool> RegisterAsync(RegisterDto registerDto); // ← добавили метод регистрации
    }
}
