
namespace UniversitySchedule.Application.DTOs
{
    public class PasswordResetToken
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
