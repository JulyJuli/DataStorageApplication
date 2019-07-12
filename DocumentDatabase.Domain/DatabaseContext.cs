using DocumentDatabase.Extensibility.DatabaseModels;
using DocumentDatabase.Extensibility.Domain;
using DocumentDatabase.Extensibility.DTOs;
using DocumentDatabase.Extensibility.Factories;
using DocumentDatabase.Extensibility.Helpers;
using System.IO;

namespace DocumentDatabase.Domain
{
    public class DatabaseContext<TModel> : IDatabaseContext<TModel>
        where TModel : ModelIdentifier
    {
        private IFileProcessingHelper fileProcessingHelper;
        private readonly IFileExtentionFactoryRetriever<TModel> fileExtentionFactoryRetriever;

        public DatabaseContext(
          IFileProcessingHelper fileProcessingHelper,
          IFileExtentionFactoryRetriever<TModel> fileExtentionFactoryRetriever)
        {
            this.fileProcessingHelper = fileProcessingHelper;
            this.fileExtentionFactoryRetriever = fileExtentionFactoryRetriever;
        }
        
        public string CreateEmptyFile(string databaseFileName, DatabaseOptions databaseOptions)
        {
            string databaseFolderPath = this.GetDatabaseFolderPath(databaseOptions);
            string path = this.fileProcessingHelper.FormFullPath(databaseFolderPath, databaseFileName, databaseOptions.DatabaseExtention);
            if (!Directory.Exists(databaseFolderPath))
            {
                Directory.CreateDirectory(databaseFolderPath);
                File.Create(path);
            }
            return path;
        }

        public string GetDatabaseFolderPath(DatabaseOptions databaseOptions)
        {
            return this.fileProcessingHelper.GetPathToDestinationFolder(databaseOptions.DatabaseExtention, typeof(TModel).Name, databaseOptions.FolderName);
        }

        public string GetFullFilePath(DatabaseOptions databaseOptions, string fileName)
        {
            return this.fileProcessingHelper.FormFullPath(this.GetDatabaseFolderPath(databaseOptions), fileName, databaseOptions.DatabaseExtention);
        }
    }
}
