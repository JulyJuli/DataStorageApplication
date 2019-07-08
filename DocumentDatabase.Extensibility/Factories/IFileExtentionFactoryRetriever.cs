using DocumentDatabase.Extensibility.Converters.ModelConverters;
using DocumentDatabase.Extensibility.DatabaseModels;

namespace DocumentDatabase.Extensibility.Factories
{
    public interface IFileExtentionFactoryRetriever<TModel>
        where TModel : ModelIdentifier
    {
        IModelConverterBase<TModel> LoadRequiredConverter(string extention);
    }
}
