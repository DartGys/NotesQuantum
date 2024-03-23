using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.DL.Data;

namespace Notes.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            string migrationsAssemblyName = typeof(NotesDbContext).Assembly.GetName().Name;

            services.AddDbContext<NotesDbContext>(options =>
                options.UseNpgsql(connectionString, npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(migrationsAssemblyName);
                })
            );

            return services;
        }
    }
}
