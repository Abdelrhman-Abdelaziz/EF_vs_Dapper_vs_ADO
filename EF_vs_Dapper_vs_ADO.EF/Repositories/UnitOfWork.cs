using EF_vs_Dapper_vs_ADO.Core;
using EF_vs_Dapper_vs_ADO.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_vs_Dapper_vs_ADO.EF.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _context;

        public IEmployeeRepository Employees { get; }
        public IDepartmentRepository Departments { get; }

        public UnitOfWork(AppDBContext context)
        {
            _context = context;
            Employees = new EmployeeRepository(_context);
            Departments = new DepartmentRepository(_context);
        }

    }
}
