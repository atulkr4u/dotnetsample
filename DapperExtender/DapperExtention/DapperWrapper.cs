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

        public static T Query<T>(object parameters)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                //var command = parameters.QueryName;

                var command = parameters.GetType().GetCustomAttribute<QueryNameAttribute>().QueryName;

                return connection.QuerySingleOrDefault<T>(command, parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public async static Task<IEnumerable<T>> QueryAll<T>(object parameters)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                //var command = parameters.QueryName;

                var command = parameters.GetType().GetCustomAttribute<QueryNameAttribute>().QueryName;

                return await connection.QueryAsync<T>(command, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}

