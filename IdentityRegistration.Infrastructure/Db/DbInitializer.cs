using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IdentityRegistration.Infrastructure.Db;

public class DbInitializer
{
    public static async Task InitializeDatabase(IServiceProvider serviceProvider, IdentityRegistrationDbContext context)
    {

        #region Migrations
        using IServiceScope serviceScope = serviceProvider.CreateScope();

        try
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            DbInitializer initializer = new();
        }
        catch (Exception ex)
        {
            ILogger<IdentityRegistrationDbContext> logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<IdentityRegistrationDbContext>>();



            logger.LogError(ex, "An error occurred while migrating or seeding the database.");



            throw;
        }
        #endregion
    }
}