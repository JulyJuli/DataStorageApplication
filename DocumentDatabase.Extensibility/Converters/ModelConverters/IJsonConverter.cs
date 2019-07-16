using DocumentDatabase.Extensibility.DTOs;

namespace DocumentDatabase.Extensibility.Converters.ModelConverters
{
    public interface IJsonConverter<TModel> : IModelConverterBase<TModel>
        where TModel : ModelIdentifier
    {
    }
}