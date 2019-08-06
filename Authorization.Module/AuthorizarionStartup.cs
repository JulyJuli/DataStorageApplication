using Authorization.Domain;
using Authorization.Domain.Context;
using Authorization.Extensibility.Providers;
using Authorization.Extensibility.Repository;
using Authorization.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authorization.Module
{
    public static class AuthorizarionStartup
    {
        public static IServiceCollection ConfigureUserCredentialDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AuthorizationContext>(options =>  options.UseSqlServer(connectionString));

            services.Configure<PasswordHasherOptions>(options => options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3);

            return services
                .AddSingleton(typeof(IPasswordHasher<>), typeof(PasswordHasher<>))
                .AddTransient<IAuthorizationProvider, AuthorizationProvider>()
                .AddTransient<IUserRepository, UserRepository>();
        }
    }
}
