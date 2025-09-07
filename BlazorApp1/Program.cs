using BlazorApp1.Components;
using BlazorApp1;
using Microsoft.EntityFrameworkCore;
using BlazorApp1.Data;
using BlazorApp1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()));

builder.Services.AddScoped<IUserService, UserService>();

builder.Logging.AddConsole();

var app = builder.Build();

try
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        logger.LogInformation("Checking database connection...");

        var retryCount = 0;
        while (retryCount < 30)
        {
            try
            {
                await context.Database.CanConnectAsync();
                break;
            }
            catch
            {
                retryCount++;
                logger.LogWarning($"Database connection attempt {retryCount}/30 failed. Retrying in 2 seconds...");
                await Task.Delay(2000);
            }
        }
      await context.Database.EnsureCreatedAsync();
        logger.LogInformation("Database ensured created!");

        if (!await context.Users.AnyAsync())
        {
            context.Users.AddRange(new[]
            {
                new BlazorApp.Models.Users { Name = "John Doe", Email = "john@example.com", Department = "IT" },
                new BlazorApp.Models.Users { Name = "Jane Smith", Email = "jane@example.com", Department = "HR" },
                new BlazorApp.Models.Users { Name = "Bob Johnson", Email = "bob@example.com", Department = "Finance" }
            });
            await context.SaveChangesAsync();
            logger.LogInformation("Test data added to database!");
        }
    }
}
catch (Exception ex)
{
    app.Logger.LogError(ex, "Database initialization failed!");
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();