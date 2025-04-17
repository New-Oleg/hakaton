using System.ComponentModel.DataAnnotations;

namespace UniversitySchedule.Domain.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public int GroupId { get; set; }

    }
}
