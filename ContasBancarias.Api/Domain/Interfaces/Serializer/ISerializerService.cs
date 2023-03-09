using System.Xml;

namespace ContasBancarias.Domain.Interfaces.Serializer
{
    public interface ISerializerService
    {
        T DeserializeObject<T>(string json);
        string SerializeObject<T>(T obj);
        T PartialDeserializeObject<T>(string json, string child);
        T DeserializeXml<T>(string xml) where T : class;
        XmlDocument SerializeXmlDocument<T>(T obj);
        string SerializeXml<T>(T obj);
    }
}
