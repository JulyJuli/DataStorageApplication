using DocumentDatabase.Extensibility.Converters.ModelConverters;
using DocumentDatabase.Extensibility.DatabaseModels;
using System;
using System.IO;
using System.Xml.Serialization;

namespace DocumentDatabase.Service.Converters
{
    public class XmlConverter<TModel> : IXmlConverter<TModel>, IModelConverterBase<TModel>
        where TModel : ModelIdentifier
    {
        public string Extention => ".xml";

        public TModel Deserialize(StreamReader streamReader)
        {
            throw new NotImplementedException();
        }

        public void Serialize(StreamWriter streamWriter, TModel fileContent)
        {
            new XmlSerializer(typeof(TModel)).Serialize(streamWriter, fileContent);
        }
    }
}