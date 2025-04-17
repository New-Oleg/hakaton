using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using UniversitySchedule.Infrastructure.Data;
using UniversitySchedule.Application.Interfaces.Services;
using UniversitySchedule.UniversitySchedule.Application.DTOs.ResetPassword;
using UniversitySchedule.Application.DTOs;
using Microsoft.EntityFrameworkCore;

namespace UniversitySchedule.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IEmailSender _emailSender;
        private readonly IUserService _userService;

        public PasswordController(AppDbContext db, IEmailSender emailSender, IUserService userService)
        {
            _db = db;
            _emailSender = emailSender;
            _userService = userService;
        }

        [HttpPost("forgot")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null) return Ok(); // Не раскрываем наличие пользователя

            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            var resetToken = new PasswordResetToken
            {
                Email = request.Email,
                Token = token,
                ExpirationTime = DateTime.UtcNow.AddHours(1)
            };

            _db.PasswordResetTokens.Add(resetToken);
            await _db.SaveChangesAsync();

            var resetUrl = $"https://yourdomain.com/reset-password?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(request.Email)}";
            var html = $"<p>Для сброса пароля перейдите по ссылке: <a href='{resetUrl}'>сбросить</a></p>";

            await _emailSender.SendEmailAsync(request.Email, "Сброс пароля", html);

            return Ok("Письмо отправлено");
        }

        [HttpPost("reset")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var reset = await _db.PasswordResetTokens
                .FirstOrDefaultAsync(x => x.Email == request.Email && x.Token == request.Token);

            if (reset == null || reset.ExpirationTime < DateTime.UtcNow)
                return BadRequest("Неверный или просроченный токен");

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null) return BadRequest("Пользователь не найден");

            string salt;
            user.PasswordHash = _userService.HashPassword(request.NewPassword, out salt);
            user.PasswordSalt = salt;

            _db.PasswordResetTokens.Remove(reset);
            _db.Users.Update(user);
            await _db.SaveChangesAsync();

            return Ok("Пароль обновлён");
        }
    }
}
