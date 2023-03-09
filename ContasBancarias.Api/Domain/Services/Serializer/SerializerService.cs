using System.Xml.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Xml;
using System.IO;
using System;
using ContasBancarias.Domain.Interfaces.Serializer;

namespace ContasBancarias.Domain.Services.Serializer
{
    public class SerializerService : ISerializerService
    {
        public T DeserializeObject<T>(string json) => JsonConvert.DeserializeObject<T>(json);

        public string SerializeObject<T>(T obj) => JsonConvert.SerializeObject(obj);

        public T PartialDeserializeObject<T>(string json, string child)
        {
            JObject jsonObject = JObject.Parse(json);
            return jsonObject.GetValue(child, System.StringComparison.OrdinalIgnoreCase).ToObject<T>();
        }

        public XmlDocument SerializeXmlDocument<T>(T obj)
        {
            var document = new XmlDocument();
            var navigator = document.CreateNavigator();

            using (var writter = navigator.AppendChild())
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writter, obj);
            }

            return document;
        }

        public string SerializeXml<T>(T obj)
        {
            var sw = new StringWriter();
            XmlTextWriter xmlTw = null;

            try
            {
                var serializer = new XmlSerializer(obj.GetType());
                xmlTw = new XmlTextWriter(sw);
                serializer.Serialize(xmlTw, obj);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                xmlTw?.Close();
                sw.Close();
            }

            return sw.ToString();
        }

        public T DeserializeXml<T>(string xml) where T : class
        {
            StringReader stReader = null;
            XmlTextReader xmlReader = null;
            T result = default;

            try
            {
                stReader = new StringReader(xml);
                var serializer = new XmlSerializer(typeof(T));
                xmlReader = new XmlTextReader(stReader);
                result = serializer.Deserialize(xmlReader) as T;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                xmlReader?.Close();
                stReader?.Close();
            }

            return result;
        }
    }
}
