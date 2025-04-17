using Newtonsoft.Json;
using System.Text;
using Xunit;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

namespace UniversitySchedule.test
{
    namespace UniversitySchedule.Tests
    {
        public class PasswordControllerTests : IClassFixture<WebApplicationFactory<Program>>
        {
            private readonly HttpClient _client;

            public PasswordControllerTests(WebApplicationFactory<Program> factory)
            {
                _client = factory.CreateClient();
            }

            [Fact]
            public async Task ForgotPassword_ShouldReturnOk_WhenValidEmail()
            {
                // Arrange
                var request = new
                {
                    Email = "user@example.com" // ❗ Укажи актуальный email из твоей БД
                };

                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Act
                var response = await _client.PostAsync("/api/password/forgot", content);

                // Assert
                response.EnsureSuccessStatusCode(); // Ожидаем 200 OK
            }

            [Fact]
            public async Task ResetPassword_ShouldReturnOk_WhenValidData()
            {
                // Arrange
                var request = new
                {
                    Email = "user@example.com", // ❗ Укажи актуальный email
                    Token = "valid_token",      // ❗ Токен должен быть актуальным из БД
                    NewPassword = "newStrongPassword123"
                };

                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Act
                var response = await _client.PostAsync("/api/password/reset", content);

                // Assert
                response.EnsureSuccessStatusCode(); // Ожидаем 200 OK
            }
        }
    }
}
