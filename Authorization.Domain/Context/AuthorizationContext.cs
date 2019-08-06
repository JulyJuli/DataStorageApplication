using Authorization.Extensibility.LoginModels;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Domain.Context
{
    public class AuthorizationContext : DbContext
    {
        public AuthorizationContext(DbContextOptions<AuthorizationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<UserDTO> Users { get; set; }
    }
}