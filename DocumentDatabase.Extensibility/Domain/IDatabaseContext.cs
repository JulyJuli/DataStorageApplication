using DocumentDatabase.Extensibility.DatabaseModels;
using DocumentDatabase.Extensibility.DTOs;

namespace DocumentDatabase.Extensibility.Domain
{
    public interface IDatabaseContext<TModel>
        where TModel : ModelIdentifier
    {
        string CreateEmptyFile(string databaseFileName, DatabaseOptions databaseOptions);

        string GetDatabaseFolderPath(DatabaseOptions databaseOptions);

        string GetFullFilePath(DatabaseOptions databaseOptions, string fileName);
    }
}
