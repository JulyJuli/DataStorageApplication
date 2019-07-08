using DocumentDatabase.Extensibility.DatabaseModels;

namespace DocumentDatabase.Extensibility.Converters.ModelConverters
{
    public interface IXmlConverter<TModel> : IModelConverterBase<TModel>
        where TModel : ModelIdentifier
    {
    }
}