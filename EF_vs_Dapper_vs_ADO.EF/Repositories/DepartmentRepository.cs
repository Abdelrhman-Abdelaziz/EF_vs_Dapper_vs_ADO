using EF_vs_Dapper_vs_ADO.Core;
using EF_vs_Dapper_vs_ADO.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_vs_Dapper_vs_ADO.EF.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly DbContext _context;

        public DepartmentRepository(DbContext context) : base(context)
        {
            _context = context;
        }
    }
}
