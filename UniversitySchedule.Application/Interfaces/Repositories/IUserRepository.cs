using System.Threading.Tasks;
using UniversitySchedule.Domain.Entities;

namespace UniversitySchedule.Application.Interfaces.Repositories
{
    public interface IUserRepository<T>
    {
        Task<User> GetByUsernameAsync(string username);
        Task AddAsync(T user);
    }
}
