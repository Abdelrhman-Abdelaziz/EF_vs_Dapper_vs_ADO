using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_vs_Dapper_vs_ADO.ADO
{
    public static class CommandHelper
    {
        public static readonly string _connStr = "Server=.;Database=SmallCompany;Trusted_Connection=true;TrustServerCertificate=True";

        public static async Task<int> ExecuteNonQueryAsync(string query, CommandType type = CommandType.Text)
        {
            using (var connection = new SqlConnection(_connStr))
            {
                var sqlCmd = new SqlCommand(query, connection);
                sqlCmd.CommandType = type;
                connection.Open();
                int res = await sqlCmd.ExecuteNonQueryAsync();

                return res;
            }
        }

        public async static Task<object> ExecuteScalerAsync(string query, CommandType type = CommandType.Text)
        {
            using (var connection = new SqlConnection(_connStr))
            {
                var sqlCmd = new SqlCommand(query, connection);
                sqlCmd.CommandType = type;
                connection.Open();
                var res = await sqlCmd.ExecuteScalarAsync();

                return res;
            }
        }

        public async static Task<int> ExecuteNonQueryAsync(string SpName, Dictionary<string, Object> ParamList, CommandType type = CommandType.Text)
        {
            using (var connection = new SqlConnection(_connStr))
            {
                var sqlCmd = new SqlCommand(SpName, connection);
                sqlCmd.CommandType = type;
                foreach (var item in ParamList)
                {
                    sqlCmd.Parameters.AddWithValue(item.Key, item.Value);
                }

                connection.Open();
                int res = await sqlCmd.ExecuteNonQueryAsync();

                return res;
            }
        }

        public async static Task<object> ExecuteScalerAsync(string SpName, Dictionary<string, Object> ParamList, CommandType type = CommandType.Text)
        {
            using (var connection = new SqlConnection(_connStr))
            {
                var sqlCmd = new SqlCommand(SpName, connection);
                sqlCmd.CommandType = type;
                foreach (var item in ParamList)
                {
                    sqlCmd.Parameters.AddWithValue(item.Key, item.Value);
                }

                connection.Open();
                var res = await sqlCmd.ExecuteScalarAsync();

                return res;
            }
        }

        public async static Task<DataTable> ExecuteDataTableAsync(string query, CommandType type = CommandType.Text)
        {
            using (var connection = new SqlConnection(_connStr))
            {
                var sqlCmd = new SqlCommand(query, connection);
                sqlCmd.CommandType = type;
                connection.Open();
                var dataReader = await sqlCmd.ExecuteReaderAsync();

                DataTable table = new DataTable();
                table.Load(dataReader);
                return table;
            }
        }


        public async static Task<DataTable> ExecuteDataTableAsync(string SpName, Dictionary<string, Object> ParamList, CommandType type = CommandType.Text)
        {
            using (var connection = new SqlConnection(_connStr))
            {
                var sqlCmd = new SqlCommand(SpName, connection);
                sqlCmd.CommandType = type;
                foreach (var item in ParamList)
                {
                    sqlCmd.Parameters.AddWithValue(item.Key, item.Value);
                }
                connection.Open();
                var dataReader = await sqlCmd.ExecuteReaderAsync();

                DataTable table = new DataTable();
                table.Load(dataReader);
                return table;
            }
        }
    }
}
