using DocumentDatabase.Extensibility.Domain;
using DocumentDatabase.Extensibility.DTOs;
using DocumentDatabase.Extensibility.Helpers;
using System.IO;

namespace DocumentDatabase.Domain
{
    public class DatabaseContext<TModel> : IDatabaseContext<TModel>
        where TModel : ModelIdentifier
    {
        private IFileProcessingHelper fileProcessingHelper;

        public DatabaseContext(IFileProcessingHelper fileProcessingHelper)
        {
            this.fileProcessingHelper = fileProcessingHelper;
        }
        
        public string CreateEmptyFile(string databaseFileName, DatabaseOptions databaseOptions)
        {
            string databaseFolderPath = GetDatabaseFolderPath(databaseOptions);
            string path = this.fileProcessingHelper.FormFullPath(databaseFolderPath, databaseFileName, databaseOptions.DatabaseExtention);
            if (!Directory.Exists(databaseFolderPath))
            {
                Directory.CreateDirectory(databaseFolderPath);
                File.Create(path);
            }
            return path;
        }

        public string GetFullFilePath(DatabaseOptions databaseOptions, string fileName)
        {
            return this.fileProcessingHelper.FormFullPath(GetDatabaseFolderPath(databaseOptions), fileName, databaseOptions.DatabaseExtention);
        }
        
        private string GetDatabaseFolderPath(DatabaseOptions databaseOptions)
        {
            return this.fileProcessingHelper.GetPathToDestinationFolder(databaseOptions.DatabaseExtention, typeof(TModel).Name, databaseOptions.FolderName);
        }
    }
}
