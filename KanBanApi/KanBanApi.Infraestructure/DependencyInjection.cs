using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using KanBanApi.Infraestructure.DbContexts;
using KanBanApi.Application.Services;
using KanBanApi.Infraestructure.Services;
using Microsoft.AspNetCore.Identity;

namespace KanBanApi.Infraestructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<DefaultDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Postgres"),
                              mig => mig.MigrationsAssembly("KanBanApi.Infraestructure"));
        });

        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DefaultDbContext>();

        services.AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IJwtGenerator, JwtGenerator>()
                .AddScoped<IActiveSession, ActiveSession>();

        return services;
    }
}