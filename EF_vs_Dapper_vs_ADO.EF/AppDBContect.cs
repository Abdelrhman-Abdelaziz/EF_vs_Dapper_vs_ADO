using EF_vs_Dapper_vs_ADO.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_vs_Dapper_vs_ADO.EF
{
    public class AppDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.;Database=SmallCompany;Trusted_Connection=true");
        }

        public virtual DbSet<Employee>? Employees { get; set; }
        public virtual DbSet<Department>? Departments { get; set; }
    }
}
