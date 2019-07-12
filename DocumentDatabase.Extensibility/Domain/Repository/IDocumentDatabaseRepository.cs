using DocumentDatabase.Extensibility.DatabaseModels;
using DocumentDatabase.Extensibility.Helpers;
using System.Collections.Generic;

namespace DocumentDatabase.Extensibility.Domain.Repository
{
    public interface IDocumentDatabaseRepository<TModel>
        where TModel : ModelIdentifier
    {
        bool DeleteFile(string id);

        bool UpdateDatabaseFiles(string id, TModel model, ModificationType modificationType);

        IList<TModel> GetAllFiles();

        void WriteFile(string id, TModel model);

        string CreateFile(TModel fileModel);
    }
}
