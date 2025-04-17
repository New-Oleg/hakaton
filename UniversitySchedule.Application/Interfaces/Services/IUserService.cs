using System.Threading.Tasks;
using UniversitySchedule.Application.DTOs.Auth;

namespace UniversitySchedule.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(RegisterDto registerDto);
    }
}
