using DocumentDatabase.Extensibility.Converters.ModelConverters;
using DocumentDatabase.Extensibility.DTOs;

namespace DocumentDatabase.Extensibility.Factories
{
    public interface IFileExtensionFactoryRetriever<TModel>
        where TModel : ModelIdentifier
    {
        IModelConverterBase<TModel> LoadRequiredConverter(string extension);
    }
}