using DocumentDatabase.Extensibility.Converters.ModelConverters;
using DocumentDatabase.Extensibility.Factories;
using DocumentDatabase.Service.Converters;
using System.Collections.Generic;
using DocumentDatabase.Extensibility.DTOs;

namespace DocumentDatabase.Service.Factories
{
    public class FileExtensionFactoryRetriever<TModel> : IFileExtensionFactoryRetriever<TModel>
      where TModel : ModelIdentifier
    {
        private Dictionary<string, IModelConverterBase<TModel>> ExtensionsConvertersMap => new Dictionary<string, IModelConverterBase<TModel>>()
        {
          {
            "json", new JsonConverter<TModel>()
          },
          {
            "xml", new XmlConverter<TModel>()
          }
        };

        public IModelConverterBase<TModel> LoadRequiredConverter(string extension)
        {
            IModelConverterBase<TModel> converterBase;
            this.ExtensionsConvertersMap.TryGetValue(extension, out converterBase);
            return converterBase;
        }
    }
}
