using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_vs_Dapper_vs_ADO.Core.Interfaces
{
    public interface IEmployeeRepository:IRepository<Employee>
    {
    }
}
