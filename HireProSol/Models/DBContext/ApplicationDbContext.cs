using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace HireProSol.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
            : base("MyContext")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}