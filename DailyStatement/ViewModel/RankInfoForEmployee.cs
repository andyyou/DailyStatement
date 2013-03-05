using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyStatement.ViewModel
{
    public class RankInfoForEmployee
    {
        public int EmployeeId { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Rank { get; set; }
        public bool RecvNotify { get; set; }
        public bool Activity { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }
}