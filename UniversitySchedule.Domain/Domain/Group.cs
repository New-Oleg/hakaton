using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversitySchedule.Domain.Entities
{
    public class Group
    {
        [Key]
        public int Id {get; set; }
        public string Name { get; set; }
        // Коллекция студентов
        public ICollection<Student> Students { get; set; }
    }
}
