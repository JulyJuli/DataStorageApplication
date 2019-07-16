using DocumentDatabase.Extensibility.Converters.ModelConverters;
using Newtonsoft.Json;
using System.IO;
using DocumentDatabase.Extensibility.DTOs;

namespace DocumentDatabase.Service.Converters
{
    public class JsonConverter<TModel> : IJsonConverter<TModel>
     where TModel : ModelIdentifier
    {
        private readonly JsonSerializer serializer;

        public JsonConverter()
        {
            serializer = new JsonSerializer();
        }

        public void Serialize(StreamWriter streamWriter, TModel fileContent)
        {
            serializer.Serialize(streamWriter, fileContent);
        }

        public TModel Deserialize(StreamReader streamReader)
        {
            return serializer.Deserialize<TModel>(new JsonTextReader(streamReader));
        }
    }
}