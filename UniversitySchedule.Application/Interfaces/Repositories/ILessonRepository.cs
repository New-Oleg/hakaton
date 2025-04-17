using System.Collections.Generic;
using System.Threading.Tasks;
using UniversitySchedule.Domain.Entities;

namespace UniversitySchedule.Application.Interfaces.Repositories
{
    public interface ILessonRepository
    {
        Task<Lesson> GetByIdAsync(int id);
        Task<IEnumerable<Lesson>> GetByTimeAsync(DateTime startTime);
        Task<IEnumerable<Lesson>> GetAllAsync();
        Task AddAsync(Lesson lesson);
        Task UpdateAsync(Lesson lesson);
        Task DeleteAsync(int id);
    }
}
