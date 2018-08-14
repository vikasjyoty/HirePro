using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace HireProSol.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<License> Licenses { get; set; }

        public DbSet<LicenseType> LicenseTypes { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Type> Types { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<ApplicationUserService> ApplicationUserServices { get; set; }

        
    }
}