using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Tools
{
    public static class ClassHelper
    {
        public static T CloneModel<T>(T classObject) where T : class
        {
            T result;
            if (classObject == null)
            {
                result = default(T);
            }
            else
            {
                object obj = Activator.CreateInstance(typeof(T));
                PropertyInfo[] properties = typeof(T).GetProperties();
                PropertyInfo[] array = properties;
                for (int i = 0; i < array.Length; i++)
                {
                    PropertyInfo propertyInfo = array[i];
                    propertyInfo.SetValue(obj, propertyInfo.GetValue(classObject, null), null);
                }
                result = (obj as T);
            }
            return result;
        }

        public static T GetAttributeValueInModel<T>(Type type, object model, string attributeName)
        {
            T result;
            if (model == null || string.IsNullOrEmpty(attributeName))
            {
                result = default(T);
            }
            else
            {
                T t = default(T);
                try
                {
                    PropertyInfo property = type.GetProperty(attributeName);
                    if (property != null)
                    {
                        t = (T)((object)property.GetValue(model, null));
                    }
                }
                catch (Exception ex)
                {
                    Loger.LogFactory.Loger().WriteError(ex.Message);  
                }
                result = t;
            }
            return result;
        }

        public static T GetAttributeValueInModel<M, T>(M model, string attributeName) where M : class
        {
            T result;
            if (model == null || string.IsNullOrEmpty(attributeName))
            {
                result = default(T);
            }
            else
            {
                T t = default(T);
                try
                {
                    PropertyInfo property = typeof(M).GetProperty(attributeName);
                    if (property != null)
                    {
                        t = (T)((object)property.GetValue(model, null));
                    }
                }
                catch (Exception ex)
                {
                    Loger.LogFactory.Loger().WriteError(ex.Message);  
                }
                result = t;
            }
            return result;
        }

        public static void SetAttributeValueInModel<T>(T model, string attributeName, object value)
        {
            try
            {
                PropertyInfo property = typeof(T).GetProperty(attributeName);
                if (property != null)
                {
                    property.SetValue(model, value, null);
                }
            }
            catch (Exception ex)
            {
                Loger.LogFactory.Loger().WriteError(ex.Message);  
            }
        }

        public static M GetModel<M>(object obj) where M : class
        {
            M result;
            if (obj == null)
            {
                result = default(M);
            }
            else
            {
                M m = default(M);
                try
                {
                    PropertyInfo[] properties = typeof(M).GetProperties();
                    PropertyInfo[] array = properties;
                    for (int i = 0; i < array.Length; i++)
                    {
                        PropertyInfo propertyInfo = array[i];
                        propertyInfo.SetValue(m, propertyInfo.GetValue(obj, null), null);
                    }
                }
                catch (Exception ex)
                {
                    Loger.LogFactory.Loger().WriteError(ex.Message);                    
                }
                result = m;
            }
            return result;
        }

        public static bool IsContainsAttribute<M, T>(string attributeName)
        {
            bool result;
            if (string.IsNullOrEmpty(attributeName))
            {
                result = false;
            }
            else
            {
                bool flag = false;
                try
                {
                    PropertyInfo property = typeof(M).GetProperty(attributeName);
                    flag = (property != null && property.PropertyType == typeof(T));
                }
                catch (Exception ex)
                {
                    Loger.LogFactory.Loger().WriteError(ex.Message);
                }
                result = flag;
            }
            return result;
        }

        public static Dictionary<string, object> GetModelDictionary<T>(T model) where T : class
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            PropertyInfo[] properties = typeof(T).GetProperties();
            PropertyInfo[] array = properties;
            for (int i = 0; i < array.Length; i++)
            {
                PropertyInfo propertyInfo = array[i];
                string name = propertyInfo.Name;
                object value = null;
                if (model != null)
                {
                    value = propertyInfo.GetValue(model, null);
                }
                if (!dictionary.ContainsKey(name))
                {
                    dictionary.Add(name, value);
                }
            }
            return dictionary;
        }

        public static void SetModelDateTimeToLocalTime<T>(T model)
        {
            if (model != null)
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                PropertyInfo[] array = properties;
                int i = 0;
                while (i < array.Length)
                {
                    PropertyInfo propertyInfo = array[i];
                    if (propertyInfo.PropertyType == typeof(DateTime))
                    {
                        DateTime dateTime = (DateTime)propertyInfo.GetValue(model, null);
                        propertyInfo.SetValue(model, dateTime.ToLocalTime(), null);
                    }
                    else if (propertyInfo.PropertyType == typeof(DateTime?))
                    {
                        DateTime? dateTime2 = (DateTime?)propertyInfo.GetValue(model, null);
                        if (dateTime2.HasValue)
                        {
                            propertyInfo.SetValue(model, dateTime2.Value.ToLocalTime(), null);
                        }
                    }
                    else
                    {
                        object value = propertyInfo.GetValue(model, null);
                        if (value != null)
                        {
                            IEnumerable enumerable = value as IEnumerable;
                            if (enumerable != null)
                            {
                                foreach (object current in enumerable)
                                {
                                    ClassHelper.SetModelDateTimeToLocalTime<object>(current);
                                }
                            }
                        }
                    }
                IL_17F:
                    i++;
                    continue;
                    goto IL_17F;
                }
            }
        }

        public static void SetListModelDateTimeToLocalTime<T>(List<T> lstModel)
        {
            if (lstModel != null)
            {
                foreach (T current in lstModel)
                {
                    ClassHelper.SetModelDateTimeToLocalTime<T>(current);
                }
            }
        }

        public static T ClassToClass<T, M>(M M1)
        {
            T t = (T)((object)Activator.CreateInstance(typeof(T)));
            T result;
            try
            {
                PropertyInfo[] properties = typeof(M).GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    PropertyInfo propertyInfo = properties[i];
                    if (!propertyInfo.PropertyType.Name.Contains("List"))
                    {
                        string name = propertyInfo.Name;
                        PropertyInfo[] properties2 = typeof(T).GetProperties();
                        for (int j = 0; j < properties2.Length; j++)
                        {
                            PropertyInfo propertyInfo2 = properties2[j];
                            name = propertyInfo2.Name;
                            if (!(propertyInfo.Name.ToUpper() != propertyInfo2.Name.ToUpper()))
                            {
                                if (propertyInfo2.PropertyType != propertyInfo.PropertyType)
                                {
                                    if (propertyInfo2.PropertyType == typeof(DateTime?) || propertyInfo2.PropertyType == typeof(DateTime))
                                    {
                                        if (propertyInfo.GetValue(M1, null) != null)
                                        {
                                            propertyInfo2.SetValue(t, propertyInfo.GetValue(M1, null).ToDate(), null);
                                        }
                                    }
                                    else if (propertyInfo2.PropertyType == typeof(int?) || propertyInfo2.PropertyType == typeof(int?) || propertyInfo2.PropertyType == typeof(int))
                                    {
                                        if (propertyInfo.GetValue(M1, null) != null)
                                        {
                                            propertyInfo2.SetValue(t, propertyInfo.GetValue(M1, null).ToIntOrNull(), null);
                                        }
                                    }
                                    else if (propertyInfo.PropertyType == typeof(int))
                                    {
                                        if (propertyInfo.GetValue(M1, null) != null)
                                        {
                                            propertyInfo2.SetValue(t, propertyInfo.GetValue(M1, null).ToString(), null);
                                        }
                                    }
                                    else if (propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?))
                                    {
                                        if (propertyInfo.GetValue(M1, null) != null)
                                        {
                                            propertyInfo2.SetValue(t, propertyInfo.GetValue(M1, null).ToDate().ToString("yyyy-MM-dd HH:mm:ss"), null);
                                        }
                                    }
                                    else
                                    {
                                        propertyInfo2.SetValue(t, propertyInfo.GetValue(M1, null), null);
                                    }
                                }
                                else
                                {
                                    propertyInfo2.SetValue(t, propertyInfo.GetValue(M1, null), null);
                                }
                                break;
                            }
                        }
                    }
                }
                result = t;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static List<string> GetPropertys<T>()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            List<string> list = new List<string>();
            PropertyInfo[] array = properties;
            for (int i = 0; i < array.Length; i++)
            {
                PropertyInfo propertyInfo = array[i];
                list.Add(propertyInfo.Name);
            }
            return list;
        }
    }
}
