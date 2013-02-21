using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace DailyStatement.Models
{
    public class EmployeeConfigurations:EntityTypeConfiguration<Employee>
    {
        public EmployeeConfigurations()
        { 
            // Fluent API 設定建置在此
        }
    }

    public class DailyInfoConfigurations : EntityTypeConfiguration<DailyInfo>
    {
        public DailyInfoConfigurations()
        {
            // Fluent API 設定建置在此
        }
    }

    public class WorkCategoryConfigurations : EntityTypeConfiguration<WorkCategory>
    {
        public WorkCategoryConfigurations()
        {
            // Fluent API 設定建置在此
        }
    }
}