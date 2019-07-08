using DocumentDatabase.Extensibility.Converters.ModelConverters;
using DocumentDatabase.Extensibility.DatabaseModels;
using System.IO;
using System.Xml.Serialization;

namespace DocumentDatabase.Service.Converters
{
    public class XmlConverter<TModel> : IXmlConverter<TModel>, IModelConverterBase<TModel>
        where TModel : ModelIdentifier
    {
        private readonly XmlSerializer xmlSerializer = new XmlSerializer(typeof(TModel));

        public TModel Deserialize(StreamReader streamReader)
        {
          return (TModel)xmlSerializer.Deserialize(streamReader);
        }

        public void Serialize(StreamWriter streamWriter, TModel fileContent)
        {
            xmlSerializer.Serialize(streamWriter, fileContent);
        }
    }
}