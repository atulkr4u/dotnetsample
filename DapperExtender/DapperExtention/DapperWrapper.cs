using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Dapper;

namespace DapperExtender.DapperExtention
{
    public static class DapperWrapper
    {
        private  static string _connectionString = "";
        public static void Configure(string connection)
        {
            _connectionString = connection;
        }

        public static async Task<T> Query<T>(this object parameters,string command)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                return connection.QuerySingleOrDefault<T>(command, parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public async static Task<IEnumerable<T>> QueryAll<T>(this object parameters,string command)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<T>(command, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}

