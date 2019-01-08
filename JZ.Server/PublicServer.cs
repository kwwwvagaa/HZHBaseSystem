using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JZ.Tools;
using JZ.Models;
using System.Reflection;
using System.Data;
using JZ.Repository;

namespace JZ.Server
{
   public class PublicServer
    {
       /// <summary>
       /// 初始化数据库连接字符串
       /// </summary>
       /// <param name="str"></param>
       public static void InitDBConnection(string str)
       {
           DbContext.Init(str, SqlSugar.DbType.SqlServer);
       }
        /// <summary>
        /// 功能描述:反射调用方法
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="action">推送广播</param>
        /// <returns>返回值</returns>
        public static string CallFunction(PostSourceEntity source)
        {
            try
            {
                Type type;
                if (string.IsNullOrEmpty(source.ClassTName))
                {
                    type = Assembly.Load("JZ.Server").GetType(string.Format("{0}.{1}", source.NameSpace, source.ClassName));
                }
                else
                {
                    type = Assembly.Load("JZ.Server").GetType(string.Format("{0}.{1}`1", source.NameSpace, source.ClassName));
                    Type typeArgument = Assembly.Load(source.TAssemblyName).GetType(source.ClassTName);

                    // MakeGenericType is badly named
                    type = type.MakeGenericType(typeArgument);
                }
                object obj = Activator.CreateInstance(type);
                if (obj == null)
                {
                    return ExcuteMessage.Error("没有找到指定的逻辑对象。");
                }

                object[] parameters = null;
                MethodInfo method = null;

                if (source.Parameters != null)
                {
                    method = type.GetMethod(source.FunctionName, new Type[] { typeof(string) });
                    parameters = new object[] { source.Parameters };
                }
                else
                {
                    method = type.GetMethod(source.FunctionName, new Type[] { });
                }
                if (method == null)
                {
                    return ExcuteMessage.Error("没有找到指定的函数。");
                }
                if (string.IsNullOrEmpty(source.MethodTName))
                {
                    object objReturn = method.Invoke(obj, parameters);
                    if (objReturn != null)
                        return objReturn.ToString();
                }
                else
                {
                    object objReturn = method.MakeGenericMethod(new Type[] { Assembly.Load(source.TAssemblyName).GetType(source.ClassTName) }).Invoke(obj, parameters);
                    if (objReturn != null)
                        return objReturn.ToString();
                }
                return "";
            }
            catch (Exception ex)
            {
                return ExcuteMessage.ErrorOfException(ex);
            }

        }


        /// <summary>
        /// 功能描述:获取缓存数据
        /// </summary>
        /// <param name="strJson">strJson</param>
        /// <returns>返回值</returns>
        public string GetCacheSource(string strJson)
        {
            if (string.IsNullOrEmpty(strJson))
                return ExcuteMessage.Error("参数为空。");

            Dictionary<string, object> lstParas = strJson.ToJsonObject<Dictionary<string, object>>();
            string strProperty = lstParas["Property"].ToString();
            string strTableName = lstParas["table"].ToString();
            string strWhere = lstParas["where"].ToString();

            Type type = Assembly.Load("JZ.Cache").GetType("JZ.Cache.CacheHelper");
            PropertyInfo pi = type.GetProperty(strProperty);

            if (pi == null)
            {
                if (string.IsNullOrWhiteSpace(strTableName))
                {
                    return ExcuteMessage.Sucess(new DataTable());
                }
                //不存在缓存对象
                PublicRepository dal = new PublicRepository();
                DataTable dt = dal.GetTableByName(strTableName, strWhere);
                return ExcuteMessage.Sucess(dt);
            }
            else if (pi.GetValue(null, null) == null)
            {
                if (string.IsNullOrWhiteSpace(strTableName))
                {
                    return ExcuteMessage.Sucess(new DataTable());
                }
                //缓存无值
                PublicRepository dal = new PublicRepository();
                DataTable dt = dal.GetTableByName(strTableName, strWhere);
                pi.SetValue(null, dt.ToJsonHasNull().ToJsonObject(pi.PropertyType), null);
                return ExcuteMessage.Sucess(dt);
            }
            else
            {
                //返回缓存内容
                return ExcuteMessage.Sucess(pi.GetValue(null, null));
            }
        }

        /// <summary>
        /// 功能描述:判断表中是否存在字段
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strColName">字段名</param>
        /// <returns>返回值</returns>
        public bool IsExistColumn(string strTableName, string strColName)
        {
            try
            {
                PublicRepository dal = new PublicRepository();
                return dal.IsExistColumn(strTableName, strColName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 功能描述:获取表字段类型
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strColName">列名</param>
        /// <returns>返回值</returns>
        public string GetColumnType(string strTableName, string strColName)
        {
            try
            {
                PublicRepository dal = new PublicRepository();
                return dal.GetColumnType(strTableName, strColName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
    }
}
