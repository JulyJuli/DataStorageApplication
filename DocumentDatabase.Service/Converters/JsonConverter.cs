using DocumentDatabase.Extensibility.Converters.ModelConverters;
using DocumentDatabase.Extensibility.DatabaseModels;
using Newtonsoft.Json;
using System.IO;

namespace DocumentDatabase.Service.Converters
{
    public class JsonConverter<TModel> : IJsonConverter<TModel>, IModelConverterBase<TModel>
     where TModel : ModelIdentifier
    {
        private readonly JsonSerializer serializer = new JsonSerializer();

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