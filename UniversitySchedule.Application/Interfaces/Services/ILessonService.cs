using System.Collections.Generic;
using System.Threading.Tasks;
using UniversitySchedule.Application.DTOs;
using UniversitySchedule.Domain.Entities;

namespace UniversitySchedule.Application.Interfaces.Services
{
    public interface ILessonService
    {
        Task<LessonDto> GetLessonByIdAsync(int id);
        Task<IEnumerable<LessonDto>> GetAllLessonsAsync();
        Task<IEnumerable<LessonDto>> GetAllByTimeAsync(DateTime startTime);
        Task CreateLessonAsync(CreateLessonDto dto);
        Task UpdateLessonAsync(UpdateLessonDto dto);
        Task DeleteLessonAsync(int id);
    }
}
