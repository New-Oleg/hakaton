using System;
using UniversitySchedule.Domain.Enums;

namespace UniversitySchedule.Application.DTOs
{
    public class CreateLessonDto
    {
        public string Subject { get; set; }

        public int auditorium { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public LessonType LessonType { get; set; }
        public int GroupId { get; set; }
        public int TeacherId { get; set; }
    }
}
