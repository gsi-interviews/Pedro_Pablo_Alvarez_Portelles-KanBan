using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using KanBanApi.Infraestructure.DbContexts;

namespace KanBanApi.Infraestructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<DefaultDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres"))
        );

        return services;
    }
}