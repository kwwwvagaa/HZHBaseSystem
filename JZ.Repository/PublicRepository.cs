using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Repository
{
    public class PublicRepository
    {
        /// <summary>
        /// 功能描述:获取Datatable
        /// </summary>
        /// <param name="strTableName">strTableName</param>
        /// <returns>返回值</returns>
        public DataTable GetTableByName(string strTableName, string strWhere)
        {
            return DbContext.Context.Db.Ado.GetDataTable(string.Format("select * from {0} {1};", strTableName, (string.IsNullOrEmpty(strWhere) ? "" : string.Format("where {0}", strWhere))));
        }

        /// <summary>
        /// 功能描述:获取DataSet
        /// </summary>
        /// <param name="lstNames">表名</param>
        /// <returns>返回值</returns>
        public DataSet GetDataSetByNames(string[] lstNames)
        {
            StringBuilder strSql = new StringBuilder();
            foreach (var item in lstNames)
            {
                strSql.AppendLine(string.Format("select * from {0};", item));
            }
            return DbContext.Context.Db.Ado.GetDataSetAll(strSql.ToString());
        }


        /// <summary>
        /// 功能描述:判断表中是否存在字段
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strColName">字段名</param>
        /// <returns>返回值</returns>
        public bool IsExistColumn(string strTableName, string strColName)
        {
            var table = DbContext.Context.Db.DbMaintenance.GetColumnInfosByTableName(strTableName);
            if (table != null)
            {
              return table.Any(p => p.DbColumnName.ToLower() == strColName.ToLower());
            }
            else
                return false;           
        }

        /// <summary>
        /// 功能描述:获取表字段类型
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strColName">列名</param>
        /// <returns>返回值</returns>
        public string GetColumnType(string strTableName, string strColName)
        {
          var table=  DbContext.Context.Db.DbMaintenance.GetColumnInfosByTableName(strTableName);
          var com= table.FirstOrDefault(p => p.DbColumnName.ToLower() == strColName.ToLower());
          if (com != null)
              return com.DataType;
          return "";
        }

        /// <summary>
        /// 功能描述:根据sql查询报表数据
        /// </summary>
        /// <param name="strSql">strSql</param>
        /// <returns>返回值</returns>
        public DataSet GetReportBySql(string strSql)
        {
            return DbContext.Context.Db.Ado.GetDataSetAll(strSql);
        }
    }
}
