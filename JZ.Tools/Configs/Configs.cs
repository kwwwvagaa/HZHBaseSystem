using System;
using System.Configuration;
using System.Linq;

namespace JZ.Tools
{
    public class Configs
    {
        public static T GetValue<T>(string name, T defaultvalue = null, string strConfigPath = "") where T : class
        {
            T t = default(T);
            T result;
            if (ConfigurationManager.AppSettings.AllKeys.Contains(name))
            {
                IConvertible convertible = ConfigurationManager.AppSettings[name];
                t = (T)((object)convertible.ToType(typeof(T), null));
                result = t;
            }
            else
            {
                if (!string.IsNullOrEmpty(strConfigPath))
                {
                    Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap
                    {
                        ExeConfigFilename = strConfigPath
                    }, ConfigurationUserLevel.None);
                    if (configuration.AppSettings.Settings.AllKeys.Contains(name))
                    {
                        result = (T)((object)Convert.ChangeType(configuration.AppSettings.Settings[name].Value, typeof(T)));
                        return result;
                    }
                }
                result = defaultvalue;
            }
            return result;
        }

        public static bool SetValue<T>(string name, T val)
        {
            bool result;
            try
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                string value = (val == null) ? "" : val.ToString();
                if (configuration.AppSettings.Settings[name] != null)
                {
                    configuration.AppSettings.Settings[name].Value = value;
                }
                else
                {
                    configuration.AppSettings.Settings.Add(name, value);
                }
                configuration.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
