namespace UniversitySchedule.Application.DTOs.Auth
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // или использовать перечисление UserRole
    }
}
