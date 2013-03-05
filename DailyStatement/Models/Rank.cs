using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DailyStatement.Models
{
    public class Rank
    {
        [Key]
        public int RankId { get; set; }

        [Required, MaxLength(50)]
        [DisplayName("權限名稱")]
        public string Name { get; set; }

        public virtual List<Employee> Employees { get; set; }
    }
}