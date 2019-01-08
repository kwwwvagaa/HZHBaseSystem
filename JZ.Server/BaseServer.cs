using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JZ.Repository;
using SqlSugar;
using JZ.Models;
using JZ.Tools;
using System.Reflection;

namespace JZ.Server
{
    public class BaseServer<TEntity> where TEntity : class, new()
    {
        private BaseRepository<TEntity> dal = new BaseRepository<TEntity>();

        public BaseServer()
        {
            this.dal.OnDalError = new Action<Exception>(this.OnError);
            this.dal.OnDalLogExecuted = new Action<string, SugarParameter[]>(this.OnLogExecuted);
            this.dal.OnDalLogExecuting = new Action<string, SugarParameter[]>(this.OnLogExecuting);
            this.dal.OnDalExecutingChangeSql = new Func<string, SugarParameter[], KeyValuePair<string, SugarParameter[]>>(this.OnExecutingChangeSql);
        }
        #region 查询数据
        /// <summary>
        /// 功能描述:数据条数
        /// </summary>
        /// <param name="strWhere">strWhere</param>
        /// <returns>返回值</returns>
        public string SelectCount(string strWhere)
        {
            try
            {
                int intCount = dal.SelectCount(strWhere);
                return ExcuteMessage.Sucess(intCount);
            }
            catch (Exception ex)
            {
                return ExcuteMessage.ErrorOfException(ex);
            }
        }
        /// <summary>
        /// 功能描述:查询数据
        /// </summary>
        /// <param name="strSqlwhere">strSqlwhere</param>
        /// <returns>返回值</returns>
        public virtual string Search(string strSqlwhere)
        {
            try
            {
                List<TEntity> list = dal.QueryList(strSqlwhere);
                return ExcuteMessage.Sucess(list);
            }
            catch (Exception ex)
            {
                return ExcuteMessage.ErrorOfException(ex);
            }
        }

        /// <summary>
        /// 功能描述:查询一条单表数据
        /// </summary>
        /// <param name="strID">ID</param>
        /// <returns>数据对象</returns>
        public virtual string Select(string strID)
        {
            try
            {
                var entity = dal.QueryByID(strID);
                return ExcuteMessage.Sucess(entity);
            }
            catch (Exception ex)
            {
                return ExcuteMessage.ErrorOfException(ex);
            }
        }

        public virtual TEntity SelectEntity(string strID)
        {
            try
            {
                var entity = dal.QueryByID(strID);
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 功能描述:根据条件查询一条数据
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>返回值</returns>
        public virtual string SelectByWhere(string strWhere)
        {
            try
            {
                var entity = dal.QueryEntity(strWhere);
                return ExcuteMessage.Sucess(entity);
            }
            catch (Exception ex)
            {
                return ExcuteMessage.ErrorOfException(ex);
            }
        }

        /// <summary>
        /// 功能描述:根据条件查询一条数据，并返回model
        /// </summary>
        /// <param name="strWhere">strWhere</param>
        /// <returns>返回值</returns>
        public virtual TEntity SelectByWhereRetModel(string strWhere)
        {
            try
            {
                return dal.QueryEntity(strWhere);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 新增数据
        /// <summary>
        /// 功能描述:新增数据
        /// </summary>
        /// <param name="strJson">strJson</param>
        /// <returns>返回值</returns>
        public virtual string Insert(string strJson)
        {
            TEntity entity = strJson.ToJsonObject<TEntity>();
            try
            {
                if (dal.Insert(entity))
                {
                    return ExcuteMessage.Sucess(entity);
                }
                else
                {
                    return ExcuteMessage.Error("新增失败。");
                }
            }
            catch (Exception ex)
            {
                return ExcuteMessage.ErrorOfException(ex);
            }

        }

        /// <summary>
        /// 功能描述:批量插入
        /// </summary>
        /// <param name="strJson">strJson</param>
        /// <returns>返回值</returns>
        public virtual string InsertLst(string strJson)
        {
            List<TEntity> entity = strJson.ToJsonObject<List<TEntity>>();
            try
            {
                if (dal.Insert(entity.ToArray()))
                {
                    return ExcuteMessage.Sucess("新增成功");
                }
                else
                {
                    return ExcuteMessage.Error("新增失败。");
                }
            }
            catch (Exception ex)
            {
                return ExcuteMessage.ErrorOfException(ex);
            }
        }

        #endregion

        #region 修改数据
        /// <summary>
        /// 功能描述:修改数据(如果实体启用记录修改字段功能，则修改主键可用，否则请重新该函数并使用dal的UpdateEx函数进行处理)
        /// </summary>
        /// <param name="strJson">strJson</param>
        /// <returns>返回值</returns>
        public virtual string Update(string strJson)
        {
            TEntity entity = strJson.ToJsonObject<TEntity>();

            try
            {
                Type type = entity.GetType();
                //取的LstModifyFields
                PropertyInfo pi = type.GetProperty("LstModifyFields");
                Dictionary<string, object> LstModifyFields = null;
                if (pi != null)
                {
                    LstModifyFields = pi.GetValue(entity, null) as Dictionary<string, object>;
                }

                //是否修改内容包含主键              
                bool blnIsHas = false;
                bool blnUpdate = false;
                if (LstModifyFields != null && LstModifyFields.Count > 0)
                {
                    List<string> lst = LstModifyFields.Keys.ToList();
                    MethodInfo miIsHasPrimaryKey = type.GetMethod("IsHasPrimaryKey", new Type[] { lst.GetType() });
                    blnIsHas = (bool)miIsHasPrimaryKey.Invoke(entity, new object[] { lst });
                }

                if (blnIsHas)
                {
                    //处理包含主键的修改
                    MethodInfo miCreateWhereDictWithModifyFields = type.GetMethod("CreateWhereDictWithModifyFields", new Type[] { });
                    Dictionary<string, object> lstDicWhere = (Dictionary<string, object>)miCreateWhereDictWithModifyFields.Invoke(entity, null);
                    blnUpdate = dal.UpdateEx(entity, LstModifyFields.Keys.ToList(), lstDicWhere);
                }
                else
                {
                    //普通修改
                    blnUpdate = dal.Update(entity, LstModifyFields == null ? null : LstModifyFields.Keys.ToList(), null);
                }

                if (pi != null)
                {
                    MethodInfo mi = entity.GetType().GetMethod("ClearRecord");
                    if (mi != null)
                    {
                        mi.Invoke(entity, null);
                    }
                }
                if (blnUpdate)
                {
                    return ExcuteMessage.Sucess(entity);
                }
                else
                {
                    return ExcuteMessage.Error("更新失败。");
                }
            }
            catch (Exception ex)
            {
                return ExcuteMessage.ErrorOfException(ex);
            }
        }

        public virtual string UpdateLst(string strJson)
        {
            List<TEntity> entity = strJson.ToJsonObject<List<TEntity>>();
            try
            {
                if (dal.Update(entity.ToArray()) > 0)
                {
                    return ExcuteMessage.Sucess("更新成功");
                }
                else
                {
                    return ExcuteMessage.Error("更新失败。");
                }
            }
            catch (Exception ex)
            {
                return ExcuteMessage.ErrorOfException(ex);
            }
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 功能描述:删除数据
        /// </summary>
        /// <param name="strJson">strJson</param>
        /// <returns>返回值</returns>
        public virtual string Delete(string strJson)
        {
            TEntity entity = strJson.ToJsonObject<TEntity>();
            try
            {
                dal.Delete(entity);
                return ExcuteMessage.Sucess(entity);
            }
            catch (Exception ex)
            {
                return ExcuteMessage.ErrorOfException(ex);
            }
        }
        /// <summary>
        /// 功能描述:根据条件删除
        /// </summary>
        /// <param name="strWhere">strJson</param>
        /// <returns>返回值</returns>
        public virtual string DeleteByWhere(string strWhere)
        {
            try
            {
                dal.Delete(strWhere);
                return ExcuteMessage.Sucess(true);
            }
            catch (Exception ex)
            {
                return ExcuteMessage.ErrorOfException(ex);
            }
        }
        #endregion

        #region 系统时间
        /// <summary>
        /// 功能描述:数据库当前时间
        /// </summary>
        /// <returns>数据库当前时间</returns>
        public string GetDBTime()
        {
            DateTime dt = dal.GetDBTime();
            return ExcuteMessage.Sucess(dt);
        }
        #endregion

        #region 事件
        /// <summary>
        /// 功能描述:Sql执行完发生
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="pars">pars</param>
        public virtual void OnLogExecuted(string sql, SugarParameter[] pars)
        {

        }
        /// <summary>
        /// 功能描述:Sql执行前发生
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="pars">pars</param>
        public virtual void OnLogExecuting(string sql, SugarParameter[] pars)
        {

        }
        /// <summary>
        /// 功能描述:执行SQL 错误时发生
        /// </summary>
        /// <param name="ex">错误</param>
        public virtual void OnError(Exception ex)
        {

        }

        /// <summary>
        /// 功能描述:SQL执行前 可以修改SQL
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="pars">pars</param>
        /// <returns>返回值</returns>
        public virtual KeyValuePair<string, SugarParameter[]> OnExecutingChangeSql(string sql, SugarParameter[] pars)
        {
            return new KeyValuePair<string, SugarParameter[]>(sql, pars);
        }
        #endregion

        #region 执行指定的Sql
        /// <summary>
        /// 功能描述:执行指定的Sql
        /// </summary>
        /// <param name="strSql">Sql</param>
        /// <param name="lstParameters">参数列表</param>
        /// <returns>是否成功</returns>
        public bool ExecuteBySql(string strSql, SugarParameter[] lstParameters = null)
        {
            return dal.ExecuteBySql(strSql, lstParameters) >= 0;
        }
        /// <summary>
        /// 功能描述:执行指定的Sql列表
        /// </summary>
        /// <param name="lstSql">sql列表</param>
        /// <param name="lstParameters">参数</param>
        /// <returns>是否成功</returns>
        public bool ExecuteBySql(string[] lstSql, SugarParameter[] lstParameters = null)
        {
            return dal.ExecuteBySql(lstSql, lstParameters) >= 0;
        }

        /// <summary>
        /// 功能描述:判读是否存在数据
        /// </summary>
        /// <param name="strSql">strSql</param>
        /// <param name="lstParameters">lstParameters</param>
        /// <returns>返回值</returns>
        public bool CheckeExists(string strSql, SugarParameter[] lstParameters = null)
        {
            return dal.CheckeExists(strSql, lstParameters);
        }
        #endregion

        /// <summary>
        /// 功能描述:重置缓存
        /// 作　　者:黄正辉
        /// 创建日期:2018-12-23 20:14:30
        /// 任务编号:
        /// </summary>
        /// <param name="strCacheName">strCacheName</param>
        public void ResetCache<T>(string strCacheName) where T : class ,new()
        {
            if (!string.IsNullOrEmpty(strCacheName))
            {
                LoadCache.LoadCacheData<T>(strCacheName, null);
            }
        }
    }
}
