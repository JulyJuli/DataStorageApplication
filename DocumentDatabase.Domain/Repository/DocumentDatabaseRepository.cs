using DocumentDatabase.Extensibility.Converters.ModelConverters;
using DocumentDatabase.Extensibility.DatabaseModels;
using DocumentDatabase.Extensibility.Domain;
using DocumentDatabase.Extensibility.Domain.Repository;
using DocumentDatabase.Extensibility.DTOs;
using DocumentDatabase.Extensibility.Factories;
using DocumentDatabase.Extensibility.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace DocumentDatabase.Domain.Repository
{
    public class DocumentDatabaseRepository<TModel> : IDocumentDatabaseRepository<TModel>
        where TModel : ModelIdentifier
    {
        private readonly IFileProcessingHelper fileProcessingHelper;
        private readonly IDatabaseContext<TModel> dataStoreApplicationContext;
        private readonly IFileExtentionFactoryRetriever<TModel> fileExtentionFactoryRetriever;
        private IModelConverterBase<TModel> modelConverter;
        private readonly ReaderWriterLock readerWriterLock;

        public DocumentDatabaseRepository(
          IDatabaseContext<TModel> dataStoreApplicationContext,
          IFileProcessingHelper fileProcessingHelper,
          IFileExtentionFactoryRetriever<TModel> fileExtentionFactoryRetriever)
        {
            this.dataStoreApplicationContext = dataStoreApplicationContext;
            this.fileProcessingHelper = fileProcessingHelper;
            this.fileExtentionFactoryRetriever = fileExtentionFactoryRetriever;
            readerWriterLock = new ReaderWriterLock();
        }

        public bool DeleteFile(string fileName)
        {
            string path = this.fileProcessingHelper.FormFullPathWitoutExtention(this.fileProcessingHelper.GetPathToDestinationFolder("database"), fileName);
            if (!File.Exists(path))
                return false;
            File.Delete(path);
            return true;
        }

        public void WriteFile(string fileName, TModel model)
        {
            string fullFilePath = this.dataStoreApplicationContext.GetFullFilePath(fileName);
            if (File.Exists(fullFilePath))
            {
                try
                {
                    this.readerWriterLock.AcquireWriterLock(-1);
                    this.WriteFile(model, fullFilePath);
                }
                finally
                {
                    this.readerWriterLock.ReleaseWriterLock();
                }
            }
            else
                this.WriteFile(model, fullFilePath);
        }

        public IDictionary<string, TModel> GetAllFiles(DatabaseOptions databaseOptions)
        {
            this.dataStoreApplicationContext.Connect(databaseOptions);
            return this.ReadAllExistingFiles(this.fileProcessingHelper.GetPathToDestinationFolder(this.dataStoreApplicationContext.DatabaseFolderName), this.dataStoreApplicationContext.Extention);
        }

        private IDictionary<string, TModel> ReadAllExistingFiles(
          string databaseForlderPath,
          string databaseExtention)
        {
            this.modelConverter = this.fileExtentionFactoryRetriever.LoadRequiredConverter(databaseExtention);
            Dictionary<string, TModel> dictionary = new Dictionary<string, TModel>();
            if (Directory.Exists(databaseForlderPath))
            {
                foreach (string enumerateFile in Directory.EnumerateFiles(databaseForlderPath, "*" + this.dataStoreApplicationContext.Extention))
                {
                    using (StreamReader streamReader = new StreamReader(enumerateFile))
                    {
                        TModel model = this.modelConverter.Deserialize(streamReader);
                        dictionary.Add(Path.GetFileName(enumerateFile), model);
                    }
                }
            }
            else
                this.CreateEmptyFolder(databaseForlderPath);
            return (IDictionary<string, TModel>)dictionary;
        }

        public bool UpdateDatabaseFiles(
          string fileName,
          TModel model,
          ModificationType modificationType)
        {
            switch (modificationType)
            {
                case ModificationType.UPDATE:
                    this.WriteFile(fileName, model);
                    break;
                case ModificationType.DELETE:
                    return this.DeleteFile(fileName);
            }
            return true;
        }

        private void WriteFile(TModel model, string fullPath)
        {
            using (StreamWriter streamWriter = new StreamWriter(fullPath, false))
                this.modelConverter.Serialize(streamWriter, model);
        }

        private DirectoryInfo CreateEmptyFolder(string databaseFolderPath)
        {
            return Directory.CreateDirectory(databaseFolderPath);
        }

        public string CreateFile(TModel fileModel)
        {
            string emptyFile = this.dataStoreApplicationContext.CreateEmptyFile(fileModel.Id);
            if (fileModel != null)
                this.WriteFile(emptyFile, fileModel);
            return emptyFile;
        }
    }
}