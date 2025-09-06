using Microsoft.EntityFrameworkCore;


    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

    public object TodoItems { get; internal set; }
}
