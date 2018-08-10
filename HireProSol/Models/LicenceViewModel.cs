using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace HireProSol.Models
{
    

    public class License
    {
        private static Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public License()
        {

            Key = new string(Enumerable.Repeat(chars, 20)
                            .Select(s => s[random.Next(s.Length)]).ToArray());
            CreatedOn = DateTime.Now;
            IsBlocked = false;
        }

        public int Id { get; set; }
        [StringLength(25), Required]
        public string Key { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }

        public bool IsBlocked { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? BlockedOn { get; set; }

        public int LicenseType_Id { get; set; }
        [ForeignKey("LicenseType_Id")]
        public LicenseType Type { get; set; }

    }

    public class LicenseType
    {
        public int Id { get; set; }
        [StringLength(200)]
        public string LicenseTypeDesc { get; set; }
        public int Validity { get; set; }
        public bool IsActive { get; set; }
    }
}