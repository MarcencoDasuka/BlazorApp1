using Microsoft.EntityFrameworkCore;
using BlazorApp1.Models; 

namespace BlazorApp1.Data  
{
    public class AppDbContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }  

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}