using System;
using System.ComponentModel.DataAnnotations;
using UniversitySchedule.Domain.Enums;

namespace UniversitySchedule.Domain.Entities
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int auditorium { get; set; }
        public string Subject { get; set; } //название придмета
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public LessonType LessonType { get; set; }
        public int GroupId { get; set; }
        public int TeacherId { get; set; }
    }
}
