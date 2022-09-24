
using EF_vs_Dapper_vs_ADO.Core;
using EF_vs_Dapper_vs_ADO.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_vs_Dapper_vs_ADO.ADO
{
    public class UnitOfWork : IUnitOfWork
    {
        public IEmployeeRepository Employees { get; }
        public IDepartmentRepository Departments { get; }

        public UnitOfWork()
        {
            Employees = new EmployeeRepository();
            Departments = new DepartmentRepository();
        }

    }
}
