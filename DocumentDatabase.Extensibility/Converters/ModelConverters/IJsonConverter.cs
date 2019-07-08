using DocumentDatabase.Extensibility.DatabaseModels;

namespace DocumentDatabase.Extensibility.Converters.ModelConverters
{
    public interface IJsonConverter<TModel> : IModelConverterBase<TModel>
        where TModel : ModelIdentifier
    {
    }
}