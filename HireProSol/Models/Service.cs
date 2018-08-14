using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireProSol.Models
{
    public class Service        
    {
        public Service()
        {
            IsActive = true;
        }

        public int ServiceId { get; set; }
        public string Name { get; set; }
        public string CssClass { get; set; }

        public int? Type_Id { get; set; }
        [ForeignKey("Type_Id")]
        public virtual Type Type { get; set; }

        public bool IsActive { get; set; }
        
        public virtual List<ApplicationUserService> ApplicationUserServices { get; set; }
        
    }

    public class ApplicationUserService
    {
        [Key]
        [Column(Order = 1)]
        public string Users_Id { get; set; }
        [ForeignKey("Users_Id")]
        public virtual ApplicationUser User { get; set; }

        [Key]
        [Column(Order = 2)]
        public int Service_Id { get; set; }
        [ForeignKey("Service_Id")]
        public virtual Service Service { get; set; }

        public DateTime RequestedOn { get; set; }
        public int Frequency { get; set; }

        public int Status_Id { get; set; }
        [ForeignKey("Status_Id")]
        public Status Status { get; set; }

        public string Caregiver_Id { get; set; }
        [ForeignKey("Caregiver_Id")]
        public virtual ApplicationUser Caregiver { get; set; }

    }

    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}