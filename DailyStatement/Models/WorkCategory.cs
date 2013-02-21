using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DailyStatement.Models
{
    
    public class WorkCategory
    {
        [Key]
        public int WorkCategoryId { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public List<DailyInfo> DailyInfos { get; set; }


    }
}