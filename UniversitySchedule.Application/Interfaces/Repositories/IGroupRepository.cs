using System.Collections.Generic;
using System.Threading.Tasks;
using UniversitySchedule.Domain.Entities;

namespace UniversitySchedule.Application.Interfaces.Repositories
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetAllAsync();
        Task<Group> GetByIdAsync(int id);
        Task<Group> GetByNameAsync(string name);
        Task AddAsync(Group group);
        Task UpdateAsync(Group group);
        Task DeleteAsync(int id);
    }
}
