using System.ComponentModel.DataAnnotations;
using UniversitySchedule.Domain.Enums;

namespace UniversitySchedule.Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public UserRole Role { get; set; }
    }
}
