using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniversitySchedule.Application.Interfaces.Repositories;
using UniversitySchedule.Domain.Entities;
using UniversitySchedule.Infrastructure.Data;

namespace UniversitySchedule.Infrastructure.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly AppDbContext _dbContext;

        public LessonRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Lesson lesson)
        {
            await _dbContext.Lessons.AddAsync(lesson);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var lesson = await _dbContext.Lessons.FindAsync(id);
            if (lesson != null)
            {
                _dbContext.Lessons.Remove(lesson);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Lesson>> GetAllAsync() => await _dbContext.Lessons.ToListAsync();

        public async Task<Lesson> GetByIdAsync(int id) => await _dbContext.Lessons.FindAsync(id);
        public async Task<IEnumerable<Lesson>> GetByTimeAsync(DateTime startTime)
        {
            return await _dbContext.Lessons
                .Where(lesson => lesson.StartTime > startTime)
                .ToListAsync();
        }

        public async Task UpdateAsync(Lesson lesson)
        {
            _dbContext.Lessons.Update(lesson);
            await _dbContext.SaveChangesAsync();
        }
    }
}
