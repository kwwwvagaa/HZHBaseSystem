
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml.Serialization;

namespace JZ.Tools
{
    public static class Json
    {
        private static JsonSerializerSettings m_defaultSettings;

        static Json()
		{
            Json.m_defaultSettings = null;
            Json.m_defaultSettings = new JsonSerializerSettings
			{
				MissingMemberHandling = MissingMemberHandling.Ignore,
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
				Converters = 
				{
					new IsoDateTimeConverter
					{
						DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
					}
				}
			};
		}
        public static string ToJson(this object objValue)
        {
            string result;
            if (null == objValue)
            {
                result = null;
            }
            else
            {
                result = JsonConvert.SerializeObject(objValue, Formatting.None, m_defaultSettings);
            }
            return result;
        }

        public static string ToJson(this object objValue, string[] lstProps, bool blnretain = true)
        {
            string result;
            if (null == objValue)
            {
                result = null;
            }
            else
            {
                JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Converters = 
					{
						new IsoDateTimeConverter
						{
							DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
						}
					}
                };
                JsonSerializerSettings jsonSerializerSettings2 = new JsonSerializerSettings();
                jsonSerializerSettings.ContractResolver = new LimitPropsContractResolver(lstProps, blnretain);
                result = JsonConvert.SerializeObject(objValue, Formatting.None, jsonSerializerSettings);
            }
            return result;
        }

        public static T ToJsonObject<T>(this string jsonString)
        {
            T result;
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                result = default(T);
            }
            else
            {
                result = JsonConvert.DeserializeObject<T>(jsonString);
            }
            return result;
        }

        public static object ToJsonObject(this string jsonString, Type type)
        {
            object result;
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                result = null;
            }
            else
            {
                result = JsonConvert.DeserializeObject(jsonString, type);
            }
            return result;
        }

        public static string ToJsonHasNull(this object objValue)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Converters = 
				{
					new IsoDateTimeConverter
					{
						DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
					}
				},
                NullValueHandling = NullValueHandling.Include
            };
            return JsonConvert.SerializeObject(objValue, Formatting.None, settings);
        }

        public static T ToXmlObj<T>(this string xml)
        {
            T result;
            try
            {
                using (StringReader stringReader = new StringReader(xml))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    result = (T)((object)xmlSerializer.Deserialize(stringReader));
                }
            }
            catch
            {
                result = default(T);
            }
            return result;
        }

        public static object Deserialize(Type type, Stream stream)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(type);
            return xmlSerializer.Deserialize(stream);
        }

        public static string ToXml<T>(this object obj)
        {
            MemoryStream memoryStream = new MemoryStream();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            try
            {
                xmlSerializer.Serialize(memoryStream, obj);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            memoryStream.Position = 0L;
            StreamReader streamReader = new StreamReader(memoryStream);
            string result = streamReader.ReadToEnd();
            streamReader.Dispose();
            memoryStream.Dispose();
            return result;
        }

        public static void Serialize<T>(this object obj, StreamWriter stream)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            xmlSerializer.Serialize(stream, obj);
        }
    }
}
