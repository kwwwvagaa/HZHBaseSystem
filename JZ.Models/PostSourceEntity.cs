//*******************************************
// 版权所有 黄正辉
// 文件名称：PostSourceEntity.cs
// 作　　者：黄正辉
// 创建日期：2018-12-23 18:34:06
// 功能描述：提交数据模型
// 任务编号：
//*******************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Models
{
    /// <summary>
    /// 功能描述:提交数据模型
    /// 作　　者:黄正辉
    /// 创建日期:2018-12-23 18:34:13
    /// 任务编号:
    /// </summary>
    public class PostSourceEntity
    {
        private string _nameSpace = "JZ.Server";

        public PostSourceEntity()
        {

        }

        /// <summary>
        /// 命名空间名称
        /// </summary>
        public string NameSpace
        {
            get { return _nameSpace; }
            set { _nameSpace = value; }
        }
        private string _className;

        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }
        private string _functionName;

        /// <summary>
        /// 方法名
        /// </summary>
        public string FunctionName
        {
            get { return _functionName; }
            set { _functionName = value; }
        }
        private string _parameters;

        /// <summary>
        /// 参数
        /// </summary>
        public string Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }


        private string _classTName;
        /// <summary>
        /// 类泛型类型名称
        /// </summary>
        public string ClassTName
        {
            get { return _classTName; }
            set { _classTName = value; }
        }

        private string _methodTName;
        /// <summary>
        /// 函数泛型名称
        /// </summary>
        public string MethodTName
        {
            get { return _methodTName; }
            set { _methodTName = value; }
        }

        private string _tAssemblyName;
        /// <summary>
        /// 泛型T的程序集名称
        /// </summary>
        public string TAssemblyName
        {
            get { return _tAssemblyName; }
            set { _tAssemblyName = value; }
        }
    }
}
