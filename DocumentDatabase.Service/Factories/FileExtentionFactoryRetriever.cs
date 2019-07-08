using DocumentDatabase.Extensibility.Converters.ModelConverters;
using DocumentDatabase.Extensibility.DatabaseModels;
using DocumentDatabase.Extensibility.Factories;
using DocumentDatabase.Service.Converters;
using System.Collections.Generic;

namespace DocumentDatabase.Service.Factories
{
    public class FileExtentionFactoryRetriever<TModel> : IFileExtentionFactoryRetriever<TModel>
      where TModel : ModelIdentifier
    {
        private Dictionary<string, IModelConverterBase<TModel>> ExtentionsConvertersMap => new Dictionary<string, IModelConverterBase<TModel>>()
        {
          {
            "json",
            (IModelConverterBase<TModel>) new JsonConverter<TModel>()
          },
          {
            "xml",
            (IModelConverterBase<TModel>) new XmlConverter<TModel>()
          }
        };

        public IModelConverterBase<TModel> LoadRequiredConverter(string extention)
        {
            IModelConverterBase<TModel> converterBase;
            this.ExtentionsConvertersMap.TryGetValue(extention, out converterBase);
            return converterBase;
        }
    }
}
