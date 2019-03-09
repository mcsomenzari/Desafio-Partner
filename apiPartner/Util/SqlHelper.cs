using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace apiPartner.Util
{
    public static class SqlHelper
    {
        public static string ExecuteProcedureReturnString(string connString, string procName, params SqlParameter[] paramaters)
        {
            string result = "";
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = procName;
                    if (paramaters != null)
                    {
                        command.Parameters.AddRange(paramaters);
                    }
                    sqlConnection.Open();
                    object ret = command.ExecuteScalar();
                    if (ret != null)
                        result = Convert.ToString(ret);
                }
            }
            return result;
        }

        public static TData ExecuteProcedureReturnData<TData>(string connString, string procName, Func<SqlDataReader, TData> translator, params SqlParameter[] parameters)
        {
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = procName;
                    if (parameters != null)
                    {
                        sqlCommand.Parameters.AddRange(parameters);
                    }
                    sqlConnection.Open();
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        TData elements;
                        try
                        {
                            elements = translator(reader);
                        }
                        finally
                        {
                            while (reader.NextResult())
                            { }
                        }
                        return elements;
                    }
                }
            }
        }

        #region Métodos para obter valores individuais das colunas do SqlDataReader

        public static string GetNullableString(SqlDataReader reader, string colName)
        {
            return reader.IsDBNull(reader.GetOrdinal(colName)) ? null : Convert.ToString(reader[colName]);
        }

        public static int GetNullableInt32(SqlDataReader reader, string colName)
        {
            return reader.IsDBNull(reader.GetOrdinal(colName)) ? 0 : Convert.ToInt32(reader[colName]);
        }

        public static bool GetBoolean(SqlDataReader reader, string colName)
        {
            return reader.IsDBNull(reader.GetOrdinal(colName)) ? default(bool) : Convert.ToBoolean(reader[colName]);
        }

        // Este método é para verificar se a coluna existe ou não no DataReader
        public static bool IsColumnExists(this System.Data.IDataRecord dr, string colName)
        {
            try
            {
                return (dr.GetOrdinal(colName) >= 0);
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }

}
