
using EF_vs_Dapper_vs_ADO.Core.Interfaces;

namespace EF_vs_Dapper_vs_ADO.Core
{
    public interface IUnitOfWork
    {
        public IEmployeeRepository Employees { get; }
        public IDepartmentRepository Departments { get; }
    }
}
