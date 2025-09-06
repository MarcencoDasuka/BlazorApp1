using Microsoft.EntityFrameworkCore;
using BlazorApp1.Models;  // Подключи пространство имён твоей модели.

namespace BlazorApp1.Data  // Можно в отдельном namespace для организации.
{
    public class AppDbContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }  // Коллекция для работы с таблицей TodoItems.

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}