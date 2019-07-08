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

        public string Extention { get; private set; }

        public string DatabaseFolderName { get; private set; }

        public void Connect(DatabaseOptions databaseOptions)
        {
            this.Extention = databaseOptions.DatabaseExtention;
            this.DatabaseFolderName = databaseOptions.FolderName;
        }

        public string CreateEmptyFile(string databaseFileName)
        {
            string databaseFolderPath = this.GetDatabaseFolderPath();
            string path = this.fileProcessingHelper.FormFullPath(this.GetDatabaseFolderPath(), databaseFileName, this.Extention);
            if (!Directory.Exists(databaseFolderPath))
            {
                Directory.CreateDirectory(databaseFolderPath);
                File.Create(path);
            }
            return path;
        }

        public string GetDatabaseFolderPath()
        {
            return this.fileProcessingHelper.GetPathToDestinationFolder(Extention, typeof(TModel).Name, DatabaseFolderName);
        }

        public string GetFullFilePath(string fileName)
        {
            return this.fileProcessingHelper.FormFullPath(this.GetDatabaseFolderPath(), fileName, Extention);
        }
    }
}
