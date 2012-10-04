using System;
using System.Data;

namespace CQRS
{
    public static class DbHelper
    {
        public static void AddDateTimeParameter(this IDbCommand cmd, string name, DateTime value)
        {
            var param = cmd.CreateParameter();
            param.ParameterName = name;
            param.DbType = DbType.DateTime;
            param.Value = value;
            cmd.Parameters.Add(param);
        }

        public static void AddLongStringParameter(this IDbCommand cmd, string name, string value)
        {
            cmd.AddStringParameter(name, value, 4000);
        }

        public static void AddStringParameter(this IDbCommand cmd, string name, string value, int length = 50)
        {
            var param = cmd.CreateParameter();
            param.ParameterName = name;
            param.DbType = DbType.String;
            param.Size = length;
            param.Value = value ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);
        }

        public static string GetStringOrNull(this IDataRecord dbRecord, int index)
        {
            return !dbRecord.IsDBNull(index) ? dbRecord.GetString(index) : null;
        }
    }
}