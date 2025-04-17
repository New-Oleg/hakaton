using System.Collections.Generic;
using System.Threading.Tasks;
using UniversitySchedule.Domain.Entities;

namespace UniversitySchedule.Application.Interfaces.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int id);
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int id);
    }
}
