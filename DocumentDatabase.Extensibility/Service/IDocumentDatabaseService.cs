using DocumentDatabase.Extensibility.DatabaseModels;
using DocumentDatabase.Extensibility.DTOs;
using System.Collections.Generic;

namespace DocumentDatabase.Extensibility.Service
{
    public interface IDocumentDatabaseService<TModel>
        where TModel : ModelIdentifier
    {
        IList<TModel> GetAllFiles(DatabaseOptions databaseOptions);
        TModel GetFile(string fileName);
        string CreateFile(TModel file);
        bool UpdateFile(string fileName, TModel file);
        bool DeleteFile(string fileName);
    }
}
