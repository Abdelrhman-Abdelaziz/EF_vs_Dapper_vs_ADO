using EF_vs_Dapper_vs_ADO.Core;
using EF_vs_Dapper_vs_ADO.Core.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_vs_Dapper_vs_ADO.ADO
{
    public class EmployeeRepository : IEmployeeRepository
    {

        public async Task<bool> AddAsync(Employee entity)
        {

            var sql = "Insert into Employees (Name,BirthDate,Address,departmentId) VALUES (@Name,@BirthDate,@Address,@departmentId)";
            
            var param = EmployeeToParam(entity);
            int res = await CommandHelper.ExecuteNonQueryAsync(sql, param);
            return res > 0;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var sql = $"DELETE FROM Employees WHERE Id = {id}";

            int res = await CommandHelper.ExecuteNonQueryAsync(sql);
            return res > 0;
        }

        public async Task<bool> DeleteAsync(Employee entity)
        {
            if (entity == null) return false;

            return await DeleteByIdAsync(entity.Id);
        }

        public async Task<IEnumerable<Employee>?> GetAllAsync()
        {
            var sql = "SELECT * FROM Employees";

            return DataTableToEmpolyeeList(
                await CommandHelper.ExecuteDataTableAsync(sql)
                );
        }

        
        public async Task<Employee?> GetByIdAsync(int id)
        {
            var sql = $"SELECT * FROM Employees WHERE Id = {id}";

            DataRow row = (DataRow) await CommandHelper.ExecuteScalerAsync(sql);
            return DataRowToEmployee(row);
        }

        public async Task<bool> UpdateAsync(Employee entity)
        {
            var sql = "UPDATE Employees SET Name = @Name, BirthDate = @BirthDate, Address = @Address, departmentId = @departmentId WHERE Id = @Id";

            var param = EmployeeToParam(entity);
            int res =  await CommandHelper.ExecuteNonQueryAsync(sql, param);
            return res > 0;
        }

        #region Mapping Functionality

        private IEnumerable<Employee>? DataTableToEmpolyeeList(DataTable dataTable)
        {
            List<Employee> EmployeeList = new();
            foreach (DataRow row in dataTable.Rows)
            {
                EmployeeList.Add(DataRowToEmployee(row));
            }
            return EmployeeList;
        }

        private Employee DataRowToEmployee(DataRow row)
        {
            if (row == null) return null;

            Employee emp = new Employee();
            emp.Id = row.Field<int>("Id");
            emp.Name = row.Field<string>("Name");
            emp.Address = row.Field<string>("Address");
            emp.BirthDate = row.Field<DateTime>("BirthDate");
            emp.departmentId = row.Field<int>("departmentId");

            return emp;
        }
        private Dictionary<string, object> EmployeeToParam(Employee employee)
        {
            var param = new Dictionary<string, object>();
            param["Name"] = employee.Name;
            param["BirthDate"] = employee.BirthDate;
            param["Address"] = employee.Address;
            param["departmentId"] = employee.departmentId;
            return param;
        } 
        #endregion
    }
}
