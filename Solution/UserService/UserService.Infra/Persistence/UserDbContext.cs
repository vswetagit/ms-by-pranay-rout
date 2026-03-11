using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entity;
using UserService.Infra.Identity;

namespace UserService.Infra.Persistence
{
    public class UserDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
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
                .HasForeignKey(rt => rt.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Address>(entity =>
            {
                entity.HasOne<ApplicationUser>()
                .WithMany(ApplicationUser => ApplicationUser.Addresses)
                .HasForeignKey(a => a.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<ApplicationRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");

            var adminRoleId = Guid.Parse("c4a3298c-6198-4d12-bd1a-56d1d1ce0aa7");
            var customerRoleId = Guid.Parse("38b657f4-ac20-4a5c-b2a3-16dfad61c381");
            var vendorRoleId = Guid.Parse("582880c3-f554-490f-a24e-526db35cffa5");

            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                new ApplicationRole { Id = customerRoleId, Name = "Customer", NormalizedName = "CUSTOMER" },
                new ApplicationRole { Id = vendorRoleId, Name = "Vendor", NormalizedName = "VENDOR" }
            );

            builder.Entity<Client>().HasData(
                new Client { ClientId = "web", ClientName = "Web Client", Description = "Web browser clients", IsActive = true },
                new Client { ClientId = "android", ClientName = "Android Client", Description = "Android mobile app", IsActive = true },
                new Client { ClientId = "ios", ClientName = "iOS Client", Description = "iOS mobile app", IsActive = true }
            );
        }
    }
}
