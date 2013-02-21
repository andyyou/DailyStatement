using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DailyStatement.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastLoginDate { get; set; }

        [Column(TypeName="varchar"), MaxLength(50)]
        public string Rank { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, EmailAddress, MaxLength(150)]
        public string Email { get; set; }

        [Required]
        public bool RecvNotify { get; set; }

        [Required]
        public bool Activity { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public List<DailyInfo> DailyInfos { get; set; }
    }
}