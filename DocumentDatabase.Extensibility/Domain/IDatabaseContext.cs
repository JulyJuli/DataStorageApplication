using DocumentDatabase.Extensibility.DatabaseModels;
using DocumentDatabase.Extensibility.DTOs;

namespace DocumentDatabase.Extensibility.Domain
{
    public interface IDatabaseContext<TModel>
        where TModel : ModelIdentifier
    {
        void Connect(DatabaseOptions databaseOptions);

        string CreateEmptyFile(string databaseFileName);

        string Extention { get; }

        string DatabaseFolderName { get; }

        string GetDatabaseFolderPath();

        string GetFullFilePath(string fileName);
    }
}
