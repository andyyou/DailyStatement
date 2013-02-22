using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DailyStatement.Models
{
    public class DailyInfo
    {
        [Key]
        public int DailyInfoId { get; set; }

        public WorkCategory Category { get; set; }

        [ConcurrencyCheck, Required]
        public string ProjectNo { get; set; } // 案號如果未成案就給 N

        [Required, MaxLength(100)]
        public string Customer { get; set; }

        [Required]
        public string WorkContent { get; set; }
   
        public DateTime CreateDate { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public int WorkingHours { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}