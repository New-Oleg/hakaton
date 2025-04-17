using System.ComponentModel.DataAnnotations;

namespace UniversitySchedule.Domain.Entities
{
    public class Teacher : User
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
