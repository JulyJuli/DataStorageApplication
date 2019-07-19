using DocumentDatabase.Extensibility.Converters.ModelConverters;
using DocumentDatabase.Extensibility.DTOs;
using DocumentDatabase.Extensibility.Factories;
using DocumentDatabase.Service.Converters;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
            IModelConverterBase<TModel> converterBase = null;
            ExtensionsConvertersMap.TryGetValue(extension, out converterBase);

            if (converterBase == null)
            {
                throw new InvalidDataException(GetExceptionMessage(extension));
            }

            return converterBase;
        }

        private string GetExceptionMessage(string extension)
        {
            var exceptionMessage = new StringBuilder($"Passed file extention {extension} isn't recognized. Availavle extentions: "); ;
            ExtensionsConvertersMap.Keys.ToList().ForEach(key => exceptionMessage.Append(key + "; "));

            return exceptionMessage.ToString();
        }
    }
}