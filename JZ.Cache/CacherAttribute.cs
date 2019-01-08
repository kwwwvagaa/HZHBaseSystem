using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Cache
{
    /**************自定义特性类*****************/
    /// <summary>  
    /// 作用：用来说明表名是什么  
    /// AttributeUsage:说明特性的目标元素是什么  
    /// AttributeTargets.Class：代表目标元素为Class  
    /// </summary>  
    [AttributeUsage(System.AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public class CacherAttribute : Attribute
    {

        /// <summary>  
        /// 表名  
        /// </summary>  
        public string TableName { get; set; }
        /// <summary>
        /// model类型
        /// </summary>
        public Type TableType { get; set; }
        /// <summary>
        /// 缓存名称
        /// </summary>
        public string CacheName { get; set; }
        /// <summary>
        /// 初始化方法名称，如果为空 则 init + CacheName
        /// </summary>
        public string InitName { get; set; }

        #region 构造方法，可选的

        public CacherAttribute() { }
        /// <summary>
        /// 功能描述:当前缓存特性
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="taleType">model类型</param>
        /// <param name="initName">初始化方法名称，如果为空 则 init + CacheName</param>
        public CacherAttribute(string tableName, Type taleType, string initName = "")
        {
            this.TableName = tableName;
            this.InitName = initName;
            this.TableType = taleType;
        }
        #endregion
    }
}
