using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class DataParameterManager
    {

        public static SqlParameter CreateParameter(string name, object value, DbType dbType, ParameterDirection direction = ParameterDirection.Input)
        {
            return CreateSqlParameter(name, value, dbType, direction);
                
        }

        public static SqlParameter CreateParameter(string name, int size, object value, DbType dbType, ParameterDirection direction = ParameterDirection.Input)
        {
            return CreateSqlParameter(name, size, value, dbType, direction);
                
        }

        public static SqlParameter CreateParameterwithSQLDBType(string name, object value, SqlDbType sqlDBType, ParameterDirection direction = ParameterDirection.Input)
        {
            return CreateSqlParameterwithSQLDBType(name, value, sqlDBType, direction);

        }

        private static SqlParameter CreateSqlParameter(string name, object value, DbType dbType, ParameterDirection direction)
        {
            return new SqlParameter
            {
                DbType = dbType,
                ParameterName = name,
                Direction = direction,
                Value = value
            };
        }

        private static SqlParameter CreateSqlParameter(string name, int size, object value, DbType dbType, ParameterDirection direction)
        {
            return new SqlParameter
            {
                DbType = dbType,
                Size = size,
                ParameterName = name,
                Direction = direction,
                Value = value
            };
        }

        private static SqlParameter CreateSqlParameterwithSQLDBType(string name, object value, SqlDbType sqlDBType, ParameterDirection direction)
        {
            return new SqlParameter
            {
                SqlDbType= sqlDBType,
                ParameterName = name,
                Direction = direction,
                Value = value
            };
        }

    }
}
