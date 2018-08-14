using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HireProSol.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser()
        {

        }

        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }

        public int? Type_Id { get; set; }
        [ForeignKey("Type_Id")]
        public virtual Type Type { get; set; }

        public string Address { get; set; }
        [StringLength(10)]
        public string PinCode { get; set; }
        public int City_Id { get; set; }
        [ForeignKey("City_Id")]
        public virtual City City { get; set; }


        public int Age { get; set; }
        [StringLength(1000)]
        public string ProfilePicUrl { get; set; }
        [StringLength(100)]
        public string ResumeHeadline { get; set; }
        public string Desc { get; set; }

        public virtual List<ApplicationUserService> ApplicationUserServices { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class City
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
    }

    public class Type
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string TypeName { get; set; }
    }
}