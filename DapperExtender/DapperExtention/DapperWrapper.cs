using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Dapper;

namespace DapperExtender.DapperExtention
{
    public static class DapperWrapper
    {
        private static string connectionString = ""; // Replace with your actual database connection string

        public static async Task<T> Query<T>(this object parameters,string command)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.QuerySingleOrDefault<T>(command, parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public async static Task<IEnumerable<T>> QueryAll<T>(this object parameters,string command)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return await connection.QueryAsync<T>(command, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}

