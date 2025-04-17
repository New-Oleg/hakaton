using System.Collections.Generic;
using System.Threading.Tasks;
using UniversitySchedule.Domain.Entities;

namespace UniversitySchedule.Application.Interfaces.Repositories
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teacher>> GetAllAsync();
        Task<Teacher> GetByIdAsync(int id);
        Task AddAsync(Teacher teacher);
        Task UpdateAsync(Teacher teacher);
        Task DeleteAsync(int id);
    }
}
