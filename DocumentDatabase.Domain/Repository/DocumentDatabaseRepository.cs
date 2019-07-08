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
        private readonly IDatabaseContext<TModel> databaseContext;
        private readonly IFileExtentionFactoryRetriever<TModel> fileExtentionFactoryRetriever;
        private IModelConverterBase<TModel> modelConverter;
        private readonly ReaderWriterLock readerWriterLock;

        public DocumentDatabaseRepository(
          IDatabaseContext<TModel> databaseContext,
          IFileProcessingHelper fileProcessingHelper,
          IFileExtentionFactoryRetriever<TModel> fileExtentionFactoryRetriever)
        {
            this.databaseContext = databaseContext;
            this.fileProcessingHelper = fileProcessingHelper;
            this.fileExtentionFactoryRetriever = fileExtentionFactoryRetriever;
            readerWriterLock = new ReaderWriterLock();
        }

        public bool DeleteFile(string fileName)
        {
            string path = this.fileProcessingHelper.FormFullPath(
                fileProcessingHelper.GetPathToDestinationFolder(databaseContext.Extention, typeof(TModel).Name, databaseContext.DatabaseFolderName),
                fileName,
                databaseContext.Extention);

            if (!File.Exists(path))
                return false;

            File.Delete(path);
            return true;
        }

        public void WriteFile(string fileName, TModel model)
        {
            string fullFilePath = this.databaseContext.GetFullFilePath(fileName);
            if (File.Exists(fullFilePath))
            {
                try
                {
                    readerWriterLock.AcquireWriterLock(-1);
                    WriteFile(model, fullFilePath);
                }
                finally
                {
                    readerWriterLock.ReleaseWriterLock();
                }
            }
            else
                WriteFile(model, fullFilePath);
        }

        public IList<TModel> GetAllFiles(DatabaseOptions databaseOptions)
        {
            this.databaseContext.Connect(databaseOptions);
            return this.ReadAllExistingFiles(this.fileProcessingHelper.GetPathToDestinationFolder(
                databaseContext.Extention, typeof(TModel).Name, this.databaseContext.DatabaseFolderName), this.databaseContext.Extention);
        }

        private IList<TModel> ReadAllExistingFiles(
          string databaseForlderPath,
          string databaseExtention)
        {
            modelConverter = this.fileExtentionFactoryRetriever.LoadRequiredConverter(databaseExtention);

            var fileSet = new List<TModel>();
            if (Directory.Exists(databaseForlderPath))
            {
                foreach (string enumerateFile in Directory.EnumerateFiles(databaseForlderPath, "*" + this.databaseContext.Extention))
                {
                    using (StreamReader streamReader = new StreamReader(enumerateFile))
                    {
                        TModel model = this.modelConverter.Deserialize(streamReader);
                        fileSet.Add( model);
                    }
                }
            }
            else
                CreateEmptyFolder(databaseForlderPath);
            return fileSet;
        }

        public bool UpdateDatabaseFiles(
          string fileName,
          TModel model,
          ModificationType modificationType)
        {
            switch (modificationType)
            {
                case ModificationType.UPDATE:
                    WriteFile(fileName, model);
                    break;
                case ModificationType.DELETE:
                    return DeleteFile(fileName);
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
            if (fileModel != null)
            {
                string emptyFile = this.databaseContext.CreateEmptyFile(fileModel.Id);

                WriteFile(fileModel, emptyFile);
                return emptyFile;
            }
            return string.Empty;
        }
    }
}