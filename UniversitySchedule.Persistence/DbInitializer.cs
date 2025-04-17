using System.Linq;
using UniversitySchedule.Infrastructure.Data;
using UniversitySchedule.Domain.Entities;

namespace UniversitySchedule.Persistence
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            // Пример инициализации данных
            if (context.Groups.Any())
            {
                return; // БД уже инициализирована
            }

            var groups = new[]
            {
                new Group { Name = "Group A" },
                new Group { Name = "Group B" }
            };

            context.Groups.AddRange(groups);
            context.SaveChanges();

            // Аналогичная инициализация преподавателей, студентов, уроков и т.д.
        }
    }
}
