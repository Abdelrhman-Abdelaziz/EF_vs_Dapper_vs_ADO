using Dapper;
using EF_vs_Dapper_vs_ADO.Core;
using EF_vs_Dapper_vs_ADO.Core.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_vs_Dapper_vs_ADO.Dapper
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public readonly string _connStr = "Server=.;Database=SmallCompany;Trusted_Connection=true;TrustServerCertificate=True";

        public async Task<bool> AddAsync(Employee entity)
        {

            var sql = "Insert into Employees (Name,BirthDate,Address,departmentId) VALUES (@Name,@BirthDate,@Address,@departmentId)";

            using (var connection = new SqlConnection(_connStr))
            {
                var result = await connection.ExecuteAsync(sql, entity);
                return result > 0;
            }
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var sql = "DELETE FROM Employees WHERE Id = @Id";

            using (var connection = new SqlConnection(_connStr))
            {
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result > 0;
            }
        }

        public async Task<bool> DeleteAsync(Employee entity)
        {
            if (entity == null) return false;

            return await DeleteByIdAsync(entity.Id);
        }

        public async Task<IEnumerable<Employee>?> GetAllAsync()
        {
            var sql = "SELECT * FROM Employees";

            using (var connection = new SqlConnection(_connStr))
            {
                var result = await connection.QueryAsync<Employee>(sql);

                return result.ToList();
            }
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Employees WHERE Id = @Id";

            using (var connection = new SqlConnection(_connStr))
            {
                var result = await connection.QueryFirstOrDefaultAsync<Employee>(sql, new { Id = id });

                return result;
            }
        }

        public async Task<bool> UpdateAsync(Employee entity)
        {
            var sql = "UPDATE Employees SET Name = @Name, BirthDate = @BirthDate, Address = @Address, departmentId = @departmentId WHERE Id = @Id";

            using (var connection = new SqlConnection(_connStr))
            {

                var result = await connection.ExecuteAsync(sql, entity);

                return result>0;
            }
        }
    }
}
