using Microsoft.EntityFrameworkCore;
using Online_Mobile_Recharge.Models;

namespace Online_Mobile_Recharge
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDetailsModel>()
                .HasOne(u => u.ServiceProvider)
                .WithMany(s => s.UserDetails)
                .HasForeignKey(u => u.ServiceProviderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserDetailsModel>()
                .HasOne(u => u.RechargePlans)
                .WithMany(s => s.UserDetails)
                .HasForeignKey(u => u.RechargePlanId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RechargePlansModel>()
                .HasOne(r => r.ServiceProvider)
                .WithMany(s => s.RechargePlans)
                .HasForeignKey(r => r.ServiceProviderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RechargeLogsModel>()
                .HasOne(r => r.RechargePlans)
                .WithMany(p => p.RechargeLogs)
                .HasForeignKey(r => r.RechargePlanId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RechargeLogsModel>()
                .HasOne(r => r.UserDetails)
                .WithMany(u => u.RechargeLogs)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ServiceProviderModel>()
                .HasMany(s => s.RechargePlans)
                .WithOne(r => r.ServiceProvider)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<Online_Mobile_Recharge.Models.UserDetailsModel>? UserDetailsModel { get; set; }
    }
}
