using Dapper;
using EF_vs_Dapper_vs_ADO.Core;
using EF_vs_Dapper_vs_ADO.Core.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace EF_vs_Dapper_vs_ADO.Dapper
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public readonly string _connStr = "Server=.;Database=SmallCompany;Trusted_Connection=true";
        public async Task<bool> AddAsync(Department entity)
        {
            var sql = "INSERT INTO Departments (Name,Location) VALUES (@Name,@Location);";
            using(var connection = new SqlConnection(_connStr))
            {
                var res = await connection.ExecuteAsync(sql);
                return res > 0;
            }
        }

        public async Task<bool> DeleteAsync(Department entity)
        {
            if (entity == null) return false;

            return await DeleteByIdAsync(entity.Id);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var sql = "DELETE FROM Departments WHERE Id=@Id";
            using (var connection = new SqlConnection(_connStr))
            {
                var res = await connection.ExecuteAsync(sql, new { Id = id });
                return res > 0;
            }
        }

        public async Task<IEnumerable<Department>?> GetAllAsync()
        {
            var sql = "SELECT * FROM Employees";

            using (var connection = new SqlConnection(_connStr))
            {
                var result = await connection.QueryAsync<Department>(sql);

                return result.ToList();
            }
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Departments WHERE Id = @Id";

            using (var connection = new SqlConnection(_connStr))
            {
                var result = await connection.QueryFirstOrDefaultAsync<Department>(sql, new { Id = id });

                return result;
            }
        }

        public async Task<bool> UpdateAsync(Department entity)
        {
            var sql = "UPDATE Departments SET Name = @Name, BirthDate = @BirthDate, Address = @Address, departmentId = @departmentId WHERE Id = @Id";

            using (var connection = new SqlConnection(_connStr))
            {

                var result = await connection.ExecuteAsync(sql, entity);

                return result > 0;
            }
        }
    }
}
