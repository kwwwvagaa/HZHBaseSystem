using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.T4
{
    public class ModelManager
    {
        public static string sqlConnectionStr = "Server=.;Initial Catalog=JZDB;User ID=sa;Password=123456";
        SqlSugarClient _db = null;
        SqlSugar.DbType _dbType = SqlSugar.DbType.SqlServer;
        public ModelManager()
        {
            _db = new SqlSugarClient(new ConnectionConfig()
               {
                   ConnectionString = sqlConnectionStr,
                   DbType = _dbType,
                   IsAutoCloseConnection = true,
                   IsShardSameThread = true
               });
        }

        /// <summary>
        /// 得到所有表
        /// </summary>
        /// <returns></returns>
        public List<DbTableInfo> GetTableList()
        {
            var lstTalbes = _db.DbMaintenance.GetTableInfoList();
            lstTalbes.AddRange(_db.DbMaintenance.GetViewInfoList());
            return lstTalbes.OrderBy(p => p.Name).ToList();
        }
        /// <summary>
        /// 获取列
        /// </summary>
        /// <param name="strTableName"></param>
        /// <returns></returns>
        public List<DbColumnInfo> GetColumnsByTableName(string strTableName)
        {
            return _db.DbMaintenance.GetColumnInfosByTableName(strTableName);
        }

       
        /// <summary>
        /// SQL[不完善,需要的自己改造]
        /// </summary>
        /// <param name="type"></param>
        /// <param name="canNull"></param>
        /// <returns></returns>
        public string TransFromSqlType(string type, bool canNull)
        {
            type = type.ToLower();
            string strNull = canNull ? "?" : "";
            switch (type)
            {
                case "int": return "int" + strNull;
                case "bigint": return "Int64" + strNull;
                case "smallint": return "Int16" + strNull;
                case "text":
                case "char":
                case "nchar":
                case "ntext":
                case "nvarchar":
                case "varchar": return "string";
                case "varbinary":
                case "image":
                case "binary": return "Byte[]";
                case "tinyint": return "byte" + strNull;
                case "bit": return "bool" + strNull;
                case "date":
                case "time":
                case "smalldatetime":
                case "timestamp":
                case "datetime": return "DateTime" + strNull;
                case "money":
                case "numeric":
                case "smallmoney":
                case "decimal": return "decimal" + strNull;
                case "float": return "double" + strNull;
                case "real": return "Single";
                default: return "string";
            }
        }

        public string ConvertFromSqlType(string type)
        {
            type = type.ToLower();
            switch (type)
            {
                case "int": return "Convert.ToInt32";
                case "bigint": return "Convert.ToInt64";
                case "smallint": return "Convert.ToInt16";
                case "text":
                case "char":
                case "nchar":
                case "ntext":
                case "nvarchar":
                case "varchar": return "Convert.ToString";
                case "tinyint": return "Convert.ToByte";
                case "bit": return " Convert.ToBoolean";
                case "date":
                case "time":
                case "smalldatetime":
                case "timestamp":
                case "datetime": return "Convert.ToDateTime";
                case "money":
                case "numeric":
                case "smallmoney":
                case "decimal": return "Convert.ToDecimal";
                case "float": return "Convert.ToDouble";
                case "real": return "Convert.ToSingle";
                default: return "Convert.ToString";
            }
        }
    }
}
