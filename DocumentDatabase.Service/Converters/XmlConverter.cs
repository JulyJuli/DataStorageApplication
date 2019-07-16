using DocumentDatabase.Extensibility.Converters.ModelConverters;
using System.IO;
using System.Xml.Serialization;
using DocumentDatabase.Extensibility.DTOs;

namespace DocumentDatabase.Service.Converters
{
    public class XmlConverter<TModel> : IXmlConverter<TModel>
        where TModel : ModelIdentifier
    {
        private readonly XmlSerializer serializer;

        public XmlConverter()
        {
            serializer = new XmlSerializer(typeof(TModel));
        }
        public TModel Deserialize(StreamReader streamReader)
        {
          return (TModel)serializer.Deserialize(streamReader);
        }

        public void Serialize(StreamWriter streamWriter, TModel fileContent)
        {
            serializer.Serialize(streamWriter, fileContent);
        }
    }
}