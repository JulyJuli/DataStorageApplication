using DocumentDatabase.Extensibility.DatabaseModels;
using System.IO;

namespace DocumentDatabase.Extensibility.Converters.ModelConverters
{
    public interface IModelConverterBase<TModel>
        where TModel : ModelIdentifier
    {
        void Serialize(StreamWriter streamWriter, TModel model);
        TModel Deserialize(StreamReader streamReader);
    }
}