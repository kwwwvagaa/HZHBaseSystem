using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JZ.Models;
using SqlSugar;
using System.Reflection;
using JZ.Repository;

namespace JZ.Server
{
    public class LoadCache : PublicServer
    {
        /// <summary>
        /// 功能描述:加载通用缓存类数据--后台
        /// </summary>
        public static void LoadAllCacheData()
        {
            LoadCacheData<Sys_UserInfo>("LstFoodCache");//用户          
        }

        /// <summary>
        /// 加载缓存数据
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="strCacheName">Km.Common.CommonHelper.CacheHelper中缓存属性名称</param>
        /// <param name="afterAction">处理后执行的函数</param>
        public static void LoadCacheData<T>(string strCacheName, Action afterAction = null) where T : class ,new()
        {
            /*
             * 尽量不要在一个case里面初始化2个缓存，当一个case里面处理多个缓存的时候，当一个缓冲需要更新时会造成不必要的性能浪费。
             * 如果需要进行特殊处理，如使用条件、排序等，请添加case否则将自动使用default
             */
            string strWhere = string.Empty;
            switch (strCacheName)
            {
                //当需要进行特殊处理时可以单独处理，如下
                case "LstFoodCache"://系统参数     
                    SetCache<T>(strCacheName);
                    //你的特殊处理
                    break;
                default:
                    SetCache<T>(strCacheName);
                    break;
            }
            if (afterAction != null)
            {
                afterAction();
            }
        }

        /// <summary>
        /// 功能描述:设置缓存数据
        /// </summary>
        /// <param name="strProperty">Km.Common.CommonHelper.CacheHelper中属性的名称</param>
        /// <param name="strWhere">条件</param>
        /// <param name="lstParameters">参数</param>
        /// <param name="intTop">前N条数据</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        private static void SetCache<T>(
            string strProperty,
            string strWhere = "1=1",
            List<SugarParameter> lstParameters = null,
            int? intTop = null,
            string strOrderByFileds = null) where T : class ,new()
        {
            Type type = Assembly.Load("JZ.Cache").GetType("JZ.Cache.CacheHelper");
            PropertyInfo pi = type.GetProperty(strProperty);

            if (pi == null)
            {
                return;
            }
            else
            {
                BaseRepository<T> dal = new BaseRepository<T>();
                List<T> lst = dal.QueryList(strWhere, lstParameters, intTop, strOrderByFileds);
                pi.SetValue(null, lst, null);
            }
        }
    }
}
