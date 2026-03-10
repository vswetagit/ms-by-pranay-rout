using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entity;
using UserService.Infra.Identity;

namespace UserService.Infra.Persistence
{
    public class UserDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options):base(options)
        {
            
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RefreshToken>(entity =>
            {
                entity.HasOne<ApplicationUser>()
                .WithMany(ApplicationUser => ApplicationUser.RefreshTokens)
                .HasForeignKey(rt => rt.UserId);

            });
        }
    }
}
