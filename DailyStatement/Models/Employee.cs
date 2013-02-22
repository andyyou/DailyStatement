using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;

namespace DailyStatement.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [DisplayName("帳號")]
        public string Account { get; set; }

        [DisplayName("密碼")]
        public string Password { get; set; }

        [DisplayName("建立日期")]
        public DateTime CreateDate { get; set; }

        [DisplayName("最後登入日期")]
        public DateTime? LastLoginDate { get; set; }

        [Column(TypeName="varchar"), MaxLength(50)]
        [DisplayName("權限")]
        public string Rank { get; set; }

        [Required, MaxLength(50)]
        [DisplayName("姓名")]
        public string Name { get; set; }

        [Required, EmailAddress, MaxLength(150)]
        [DisplayName("電子郵件")]
        public string Email { get; set; }

        [Required]
        [DisplayName("接收通知訊息")]
        [Description("通知訊息用於通知管理員那些員工在一定天數內未登入")]
        public bool RecvNotify { get; set; }

        [Required]
        [DisplayName("啟用")]
        public bool Activity { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual List<DailyInfo> DailyInfos { get; set; }
    }

    public class SimMemberInfo
    {
        public string UserNo; //會員編號
        public string UserName; //會員名稱
        public DateTime RegDate; //註冊日期
        public int Points; //累積點數

        //模疑資料來源
        public static List<SimMemberInfo> SimuDataStore = null;

        static SimMemberInfo()
        {
            Random rnd = new Random();
            //借用具名顏色名稱來產生隨機資料
            string[] colorNames = typeof(Color)
                .GetProperties(BindingFlags.Static | BindingFlags.Public)
                .Select(o => o.Name).ToArray();
            SimuDataStore =
                colorNames
                .Select(cn => new SimMemberInfo()
                {
                    UserNo = string.Format("C{0:00000}", rnd.Next(99999)),
                    UserName = cn,
                    RegDate = DateTime.Today.AddDays(-rnd.Next(1000)),
                    Points = rnd.Next(9999)
                }).ToList();
        }
    }

}