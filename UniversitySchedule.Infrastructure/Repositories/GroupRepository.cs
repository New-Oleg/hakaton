using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversitySchedule.Application.Interfaces.Repositories;
using UniversitySchedule.Domain.Entities;
using UniversitySchedule.Infrastructure.Data;

namespace UniversitySchedule.Infrastructure.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly AppDbContext _context;

        public GroupRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Group>> GetAllAsync() => await _context.Groups.ToListAsync();

        public async Task<Group> GetByIdAsync(int id) => await _context.Groups.FindAsync(id);
        public async Task<Group> GetByNameAsync(string name) => await _context.Groups.FindAsync(name);

        public async Task AddAsync(Group group)
        {
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Group group)
        {
            _context.Groups.Update(group);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group != null)
            {
                _context.Groups.Remove(group);
                await _context.SaveChangesAsync();
            }
        }
    }
}
