using Microsoft.EntityFrameworkCore;
using UniversitySchedule.Application.Interfaces.Repositories;
using UniversitySchedule.Domain.Entities;
using UniversitySchedule.Infrastructure.Data;

namespace UniversitySchedule.UniversitySchedule.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository<Student>
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Student>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Student student)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Students.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddAsync(Student user)
        {
            await _context.Students.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
