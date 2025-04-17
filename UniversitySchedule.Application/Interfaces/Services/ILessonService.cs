using System.Collections.Generic;
using System.Threading.Tasks;
using UniversitySchedule.Application.DTOs;

namespace UniversitySchedule.Application.Interfaces.Services
{
    public interface ILessonService
    {
        Task<LessonDto> GetLessonByIdAsync(int id);
        Task<IEnumerable<LessonDto>> GetAllLessonsAsync();
        Task CreateLessonAsync(CreateLessonDto dto);
        Task UpdateLessonAsync(UpdateLessonDto dto);
        Task DeleteLessonAsync(int id);
    }
}
