using JZ.Tools;
using SqlSugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Models
{
    [Serializable]
    public class BaseEntity
    {
        private bool m_blnRecordModify = false;

        private Dictionary<string, object> m_lstModifyFields = new Dictionary<string, object>();

        private DBOperationType _identify = DBOperationType.SELECT;

        private static Dictionary<Type, List<string>> m_lstKeys = new Dictionary<Type, List<string>>();

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public bool RecortModify
        {
            get
            {
                return this.m_blnRecordModify;
            }
        }

        [SugarColumn(IsIgnore = true)]
        public Dictionary<string, object> LstModifyFields
        {
            get
            {
                if (this.ContainProperty("isUpload"))
                {
                    if (this.m_blnRecordModify)
                    {
                        this.m_lstModifyFields["isUpload"] = 0;
                    }
                    this.SetModelValue("isUpload", 0);
                }
                return this.m_lstModifyFields;
            }
            private set
            {
                this.m_lstModifyFields = value;
            }
        }

        [SugarColumn(IsIgnore = true)]
        public DBOperationType Identify
        {
            get
            {
                return this._identify;
            }
            set
            {
                this._identify = value;
            }
        }

        protected void SetValueCall(string str, object obj)
        {
            if (this.Identify == DBOperationType.SELECT)
            {
                this.Identify = DBOperationType.UPDATE;
            }
            if (this.m_blnRecordModify)
            {
                if (!this.m_lstModifyFields.ContainsKey(str))
                {
                    this.m_lstModifyFields.Add(str, obj);
                }
            }
        }

        public void BeginRecord()
        {
            this.m_blnRecordModify = true;
            this.BeginRecordByObject(this);
        }

        private void BeginRecordByObject(object objThis)
        {
            PropertyInfo[] properties = objThis.GetType().GetProperties();
            PropertyInfo[] array = properties;
            for (int i = 0; i < array.Length; i++)
            {
                PropertyInfo propertyInfo = array[i];
                object value = propertyInfo.GetValue(objThis, null);
                if (value != null)
                {
                    Type propertyType = propertyInfo.PropertyType;
                    if (propertyType.GetInterface("IEnumerable") != null && propertyType.GetInterface("IList") != null)
                    {
                        IEnumerable enumerable = value as IEnumerable;
                        foreach (object current in enumerable)
                        {
                            if (current is BaseEntity)
                            {
                                MethodInfo method = current.GetType().GetMethod("BeginRecord");
                                if (method != null)
                                {
                                    method.Invoke(current, null);
                                }
                            }
                            else if (current != null && !current.GetType().IsPrimitive && !(current is string))
                            {
                                this.BeginRecordByObject(current);
                            }
                        }
                    }
                    else if (propertyType.GetInterface("IDictionary") != null)
                    {
                        IDictionary dictionary = value as IDictionary;
                        foreach (object current2 in dictionary.Keys)
                        {
                            if (current2 is BaseEntity)
                            {
                                MethodInfo method = current2.GetType().GetMethod("BeginRecord");
                                if (method != null)
                                {
                                    method.Invoke(current2, null);
                                }
                            }
                            else if (current2 != null && !current2.GetType().IsPrimitive && !(current2 is string))
                            {
                                this.BeginRecordByObject(current2);
                            }
                        }
                        foreach (object current3 in dictionary.Values)
                        {
                            if (current3 is BaseEntity)
                            {
                                MethodInfo method = current3.GetType().GetMethod("BeginRecord");
                                if (method != null)
                                {
                                    method.Invoke(current3, null);
                                }
                            }
                            else if (current3 != null && !current3.GetType().IsPrimitive && !(current3 is string))
                            {
                                this.BeginRecordByObject(current3);
                            }
                        }
                    }
                }
            }
        }

        public void EndRecord()
        {
            this.m_blnRecordModify = false;
            this.EndRecordByObject(this);
        }

        private void EndRecordByObject(object objThis)
        {
            PropertyInfo[] properties = objThis.GetType().GetProperties();
            PropertyInfo[] array = properties;
            for (int i = 0; i < array.Length; i++)
            {
                PropertyInfo propertyInfo = array[i];
                object value = propertyInfo.GetValue(objThis, null);
                if (value != null)
                {
                    Type propertyType = propertyInfo.PropertyType;
                    if (propertyType.GetInterface("IEnumerable") != null && propertyType.GetInterface("IList") != null)
                    {
                        IEnumerable enumerable = value as IEnumerable;
                        foreach (object current in enumerable)
                        {
                            if (current is BaseEntity)
                            {
                                MethodInfo method = current.GetType().GetMethod("EndRecord");
                                if (method != null)
                                {
                                    method.Invoke(current, null);
                                }
                            }
                            else if (current != null && !current.GetType().IsPrimitive && !(current is string))
                            {
                                this.BeginRecordByObject(current);
                            }
                        }
                    }
                    else if (propertyType.GetInterface("IDictionary") != null)
                    {
                        IDictionary dictionary = value as IDictionary;
                        foreach (object current2 in dictionary.Keys)
                        {
                            if (current2 is BaseEntity)
                            {
                                MethodInfo method = current2.GetType().GetMethod("EndRecord");
                                if (method != null)
                                {
                                    method.Invoke(current2, null);
                                }
                            }
                            else if (current2 != null && !current2.GetType().IsPrimitive && !(current2 is string))
                            {
                                this.BeginRecordByObject(current2);
                            }
                        }
                        foreach (object current3 in dictionary.Values)
                        {
                            if (current3 is BaseEntity)
                            {
                                MethodInfo method = current3.GetType().GetMethod("EndRecord");
                                if (method != null)
                                {
                                    method.Invoke(current3, null);
                                }
                            }
                            else if (current3 != null && !current3.GetType().IsPrimitive && !(current3 is string))
                            {
                                this.BeginRecordByObject(current3);
                            }
                        }
                    }
                }
            }
        }

        public void ClearRecord()
        {
            this.m_lstModifyFields.Clear();
            this.ClearRecordByObject(this);
        }

        private void ClearRecordByObject(object objThis)
        {
            PropertyInfo[] properties = objThis.GetType().GetProperties();
            PropertyInfo[] array = properties;
            for (int i = 0; i < array.Length; i++)
            {
                PropertyInfo propertyInfo = array[i];
                object value = propertyInfo.GetValue(objThis, null);
                if (value != null)
                {
                    Type propertyType = propertyInfo.PropertyType;
                    if (propertyType.GetInterface("IEnumerable") != null && propertyType.GetInterface("IList") != null)
                    {
                        IEnumerable enumerable = value as IEnumerable;
                        foreach (object current in enumerable)
                        {
                            if (current is BaseEntity)
                            {
                                MethodInfo method = current.GetType().GetMethod("ClearRecord");
                                if (method != null)
                                {
                                    method.Invoke(current, null);
                                }
                            }
                            else if (current != null && !current.GetType().IsPrimitive && !(current is string))
                            {
                                this.BeginRecordByObject(current);
                            }
                        }
                    }
                    else if (propertyType.GetInterface("IDictionary") != null)
                    {
                        IDictionary dictionary = value as IDictionary;
                        foreach (object current2 in dictionary.Keys)
                        {
                            if (current2 is BaseEntity)
                            {
                                MethodInfo method = current2.GetType().GetMethod("ClearRecord");
                                if (method != null)
                                {
                                    method.Invoke(current2, null);
                                }
                            }
                            else if (current2 != null && !current2.GetType().IsPrimitive && !(current2 is string))
                            {
                                this.BeginRecordByObject(current2);
                            }
                        }
                        foreach (object current3 in dictionary.Values)
                        {
                            if (current3 is BaseEntity)
                            {
                                MethodInfo method = current3.GetType().GetMethod("ClearRecord");
                                if (method != null)
                                {
                                    method.Invoke(current3, null);
                                }
                            }
                            else if (current3 != null && !current3.GetType().IsPrimitive && !(current3 is string))
                            {
                                this.BeginRecordByObject(current3);
                            }
                        }
                    }
                }
            }
        }


        public virtual void Create()
        {
        }

        public virtual void Modify(string KeyValue)
        {
        }

        public dynamic Clone()
        {
            return Activator.CreateInstance(base.GetType());
        }

        public void CopyValueFrom(object model)
        {
            if (model != null)
            {
                PropertyInfo[] properties = base.GetType().GetProperties();
                PropertyInfo[] properties2 = model.GetType().GetProperties();
                PropertyInfo[] array = properties;
                int i = 0;
                while (i < array.Length)
                {
                    PropertyInfo myitem = array[i];
                    List<PropertyInfo> list = (from mp in properties2
                                               where mp.Name.ToLower() == myitem.Name.ToLower() && mp.PropertyType == myitem.PropertyType
                                               select mp).ToList<PropertyInfo>();
                    if (list.Count == 1)
                    {
                        if (list[0].PropertyType.Name == "List`1")
                        {
                            MethodInfo method = myitem.PropertyType.GetMethod("Clear");
                            object value = myitem.GetValue(this, null);
                            if (value != null)
                            {
                                method.Invoke(value, new object[0]);
                                IEnumerable enumerable = list[0].GetValue(model, null) as IEnumerable;
                                if (enumerable != null)
                                {
                                    foreach (object current in enumerable)
                                    {
                                        MethodInfo method2 = myitem.PropertyType.GetMethod("Add");
                                        object value2 = myitem.GetValue(this, null);
                                        if (value2 != null)
                                        {
                                            method2.Invoke(value2, new object[]
											{
												current
											});
                                        }
                                    }
                                }
                            }
                        }
                        else if (myitem.CanWrite)
                        {
                            myitem.SetValue(this, list[0].GetValue(model, null), null);
                        }
                    }
                IL_1F4:
                    i++;
                    continue;
                    goto IL_1F4;
                }
            }
        }

        public void CopyValueFromNotNull(object model)
        {
            if (model != null)
            {
                PropertyInfo[] properties = base.GetType().GetProperties();
                PropertyInfo[] properties2 = model.GetType().GetProperties();
                PropertyInfo[] array = properties;
                int i = 0;
                while (i < array.Length)
                {
                    PropertyInfo myitem = array[i];
                    List<PropertyInfo> list = (from mp in properties2
                                               where mp.Name.ToLower() == myitem.Name.ToLower() && mp.PropertyType == myitem.PropertyType && mp.GetValue(model, null) != null
                                               select mp).ToList<PropertyInfo>();
                    if (list.Count == 1)
                    {
                        if (list[0].PropertyType.Name == "List`1")
                        {
                            IEnumerable enumerable = list[0].GetValue(model, null) as IEnumerable;
                            object value = myitem.GetValue(this, null);
                            if (enumerable != "[]")
                            {
                                MethodInfo method = myitem.PropertyType.GetMethod("Clear");
                                method.Invoke(value, new object[0]);
                                foreach (object current in enumerable)
                                {
                                    MethodInfo method2 = myitem.PropertyType.GetMethod("Add");
                                    object value2 = myitem.GetValue(this, null);
                                    method2.Invoke(value2, new object[]
									{
										current
									});
                                }
                            }
                        }
                        else
                        {
                            myitem.SetValue(this, list[0].GetValue(model, null), null);
                        }
                    }
                IL_1EB:
                    i++;
                    continue;
                    goto IL_1EB;
                }
            }
        }

        public void SetModelValue(string pName, object value)
        {
            PropertyInfo[] properties = base.GetType().GetProperties();
            List<PropertyInfo> list = (from mp in properties
                                       where mp.Name == pName
                                       select mp).ToList<PropertyInfo>();
            if (list.Count > 0)
            {
                list[0].SetValue(this, value, null);
            }
        }

        public object GetModelValue(string pName)
        {
            PropertyInfo[] properties = base.GetType().GetProperties();
            List<PropertyInfo> list = (from mp in properties
                                       where mp.Name == pName
                                       select mp).ToList<PropertyInfo>();
            object result;
            if (list.Count > 0)
            {
                result = list[0].GetValue(this, null);
            }
            else
            {
                result = null;
            }
            return result;
        }

        public bool ContainProperty(string propertyName)
        {
            bool result;
            if (this != null && !string.IsNullOrEmpty(propertyName))
            {
                PropertyInfo property = base.GetType().GetProperty(propertyName);
                result = (property != null);
            }
            else
            {
                result = false;
            }
            return result;
        }

        public bool IsHasPrimaryKey(List<string> lst)
        {
            bool result;
            if (lst == null || lst.Count <= 0)
            {
                result = false;
            }
            else
            {
                this.InitPrimaryKeyList();
                Type type = base.GetType();
                result = (BaseEntity.m_lstKeys.ContainsKey(type) && BaseEntity.m_lstKeys[type].Any((string p) => lst.Contains(p)));
            }
            return result;
        }

        private void InitPrimaryKeyList()
        {
            Type type = base.GetType();
            if (!BaseEntity.m_lstKeys.ContainsKey(type) || BaseEntity.m_lstKeys[type].Count <= 0)
            {
                lock (BaseEntity.m_lstKeys)
                {
                    if (!BaseEntity.m_lstKeys.ContainsKey(type) || BaseEntity.m_lstKeys[type].Count <= 0)
                    {
                        List<string> list = new List<string>();
                        PropertyInfo[] properties = base.GetType().GetProperties();
                        PropertyInfo[] array = properties;
                        for (int i = 0; i < array.Length; i++)
                        {
                            PropertyInfo propertyInfo = array[i];
                            SugarColumn sugarColumn = (from it in propertyInfo.GetCustomAttributes(typeof(SugarColumn), true)
                                                       where it is SugarColumn
                                                       select (SugarColumn)it).FirstOrDefault<SugarColumn>();
                            if (sugarColumn != null)
                            {
                                if (sugarColumn.IsIdentity || sugarColumn.IsPrimaryKey)
                                {
                                    list.Add(propertyInfo.Name);
                                }
                            }
                        }
                        BaseEntity.m_lstKeys[type] = list;
                    }
                }
            }
        }

        public Dictionary<string, object> CreateWhereDictWithModifyFields()
        {
            Type type = base.GetType();
            this.InitPrimaryKeyList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            if (BaseEntity.m_lstKeys.ContainsKey(type) && BaseEntity.m_lstKeys[type].Count > 0)
            {
                foreach (string current in BaseEntity.m_lstKeys[type])
                {
                    dictionary.Add(current, this.m_lstModifyFields.ContainsKey(current) ? this.m_lstModifyFields[current] : ClassHelper.GetAttributeValueInModel<object>(base.GetType(), this, current));
                }
            }
            return dictionary;
        }
    }
}