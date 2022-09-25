using EF_vs_Dapper_vs_ADO.Core;
using EF_vs_Dapper_vs_ADO.Core.Interfaces;
using System.Data;


namespace EF_vs_Dapper_vs_ADO.ADO
{
    public class DepartmentRepository : IDepartmentRepository
    {

        public async Task<bool> AddAsync(Department entity)
        {
            
            var sql = "INSERT INTO Departments (Name,Location) VALUES (@Name,@Location);";

            var param = DepartmentToParam(entity);
            int res = await CommandHelper.ExecuteNonQueryAsync(sql, param);
            return res > 0;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var sql = $"DELETE FROM Departments WHERE Id = {id}";

            int res = await CommandHelper.ExecuteNonQueryAsync(sql);
            return res > 0;
        }

        public async Task<bool> DeleteAsync(Department entity)
        {
            if (entity == null) return false;

            return await DeleteByIdAsync(entity.Id);
        }

        public async Task<IEnumerable<Department>?> GetAllAsync()
        {
            var sql = "SELECT * FROM Departments";

            return DataTableToEmpolyeeList(
                await CommandHelper.ExecuteDataTableAsync(sql)
                );
        }


        public async Task<Department?> GetByIdAsync(int id)
        {
            var sql = $"SELECT * FROM Departments WHERE Id = {id}";

            var dataTable = await CommandHelper.ExecuteDataTableAsync(sql);
            return DataTableToEmpolyeeList(dataTable)?.SingleOrDefault();
        }

        public async Task<bool> UpdateAsync(Department entity)
        {
            var sql = "UPDATE Departments SET Name = @Name, Location = @Location WHERE Id = @Id";

            var param = DepartmentToParam(entity);
            int res = await CommandHelper.ExecuteNonQueryAsync(sql, param);
            return res > 0;
        }

        #region Mapping Functionality

        private IEnumerable<Department>? DataTableToEmpolyeeList(DataTable dataTable)
        {
            List<Department> DepartmentList = new();
            foreach (DataRow row in dataTable.Rows)
            {
                DepartmentList.Add(DataRowToDepartment(row));
            }
            return DepartmentList;
        }

        private Department? DataRowToDepartment(DataRow row)
        {
            if (row == null) return null;

            Department emp = new Department();
            emp.Id = row.Field<int>("Id");
            emp.Name = row.Field<string>("Name");
            emp.Location = row.Field<string>("Location");


            return emp;
        }
        private Dictionary<string, object> DepartmentToParam(Department department)
        {
            if (department == null) return null;

            var param = new Dictionary<string, object>();
            param["Id"] = department.Id;
            param["Name"] = department.Name;
            param["Location"] = department.Location;
            return param;
        }
        #endregion
    }
}
