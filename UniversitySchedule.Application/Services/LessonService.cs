using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniversitySchedule.Application.DTOs;
using UniversitySchedule.Application.Interfaces.Repositories;
using UniversitySchedule.Application.Interfaces.Services;
using UniversitySchedule.Domain.Entities;

namespace UniversitySchedule.Application.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;

        public LessonService(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<LessonDto> GetLessonByIdAsync(int id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);
            if (lesson == null)
                return null;

            return new LessonDto
            {
                Id = lesson.Id,
                Subject = lesson.Subject,
                auditorium = lesson.auditorium,
                StartTime = lesson.StartTime,
                EndTime = lesson.EndTime,
                LessonType = lesson.LessonType,
                GroupId = lesson.GroupId,
                TeacherId = lesson.TeacherId
            };
        }


        public async Task<IEnumerable<LessonDto>> GetAllLessonsAsync()
        {
            var lessons = await _lessonRepository.GetAllAsync();
            return lessons.Select(lesson => new LessonDto
            {
                Id = lesson.Id,
                Subject = lesson.Subject,
                auditorium = lesson.auditorium,
                StartTime = lesson.StartTime,
                EndTime = lesson.EndTime,
                LessonType = lesson.LessonType,
                GroupId = lesson.GroupId,
                TeacherId = lesson.TeacherId
            });
        }

        public async Task<IEnumerable<LessonDto>> GetAllByTimeAsync(DateTime startTime)
        {
            var lessons = await _lessonRepository.GetByTimeAsync(startTime);

            if (lessons == null)
                return null;

            return lessons.Select(lesson => new LessonDto
            {
                Id = lesson.Id,
                Subject = lesson.Subject,
                auditorium = lesson.auditorium,
                StartTime = lesson.StartTime,
                EndTime = lesson.EndTime,
                LessonType = lesson.LessonType,
                GroupId = lesson.GroupId,
                TeacherId = lesson.TeacherId
            });
        }

        public async Task CreateLessonAsync(CreateLessonDto dto)
        {
            var lesson = new Lesson
            {
                Subject = dto.Subject,
                auditorium = dto.auditorium,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                LessonType = dto.LessonType,
                GroupId = dto.GroupId,
                TeacherId = dto.TeacherId
            };

            await _lessonRepository.AddAsync(lesson);
        }

        public async Task UpdateLessonAsync(UpdateLessonDto dto)
        {
            var lesson = await _lessonRepository.GetByIdAsync(dto.Id);
            if (lesson == null)
                return; // Можно выбрасывать исключение

            lesson.Subject = dto.Subject;
            lesson.auditorium = dto.auditorium;
            lesson.StartTime = dto.StartTime;
            lesson.EndTime = dto.EndTime;
            lesson.LessonType = dto.LessonType;
            lesson.GroupId = dto.GroupId;
            lesson.TeacherId = dto.TeacherId;

            await _lessonRepository.UpdateAsync(lesson);
        }

        public async Task DeleteLessonAsync(int id)
        {
            await _lessonRepository.DeleteAsync(id);
        }
    }
}
