using DocumentDatabase.Extensibility.Converters.ModelConverters;
using DocumentDatabase.Extensibility.Domain;
using DocumentDatabase.Extensibility.Domain.Repository;
using DocumentDatabase.Extensibility.DTOs;
using DocumentDatabase.Extensibility.Factories;
using DocumentDatabase.Extensibility.Helpers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentDatabase.Domain.Repository
{
    public class DocumentDatabaseRepository<TModel> : IDocumentDatabaseRepository<TModel>
        where TModel : ModelIdentifier
    {
        private readonly IFileProcessingHelper fileProcessingHelper;
        private readonly IDatabaseContext<TModel> databaseContext;
        private readonly DatabaseOptions databaseOptions;

        private readonly ReaderWriterLock readerWriterLock;

        private IModelConverterBase<TModel> modelConverter;

        public DocumentDatabaseRepository(
          IOptions<DatabaseOptions> databaseOptions, 
          IDatabaseContext<TModel> databaseContext,
          IFileProcessingHelper fileProcessingHelper,
          IFileExtensionFactoryRetriever<TModel> fileExtensionFactoryRetriever)
        {
            this.databaseContext = databaseContext;
            this.fileProcessingHelper = fileProcessingHelper;
            this.databaseOptions = databaseOptions.Value;
            readerWriterLock = new ReaderWriterLock();

            modelConverter = fileExtensionFactoryRetriever.LoadRequiredConverter(this.databaseOptions.DatabaseExtention);
        }

        public Task DeleteFile(string fileName)
        {
            string path = fileProcessingHelper.FormFullPath(
                fileProcessingHelper.GetPathToDestinationFolder(databaseOptions.DatabaseExtention, typeof(TModel).Name, databaseOptions.FolderName),
                fileName,
                databaseOptions.DatabaseExtention);
            
            if (File.Exists(path))
            {
                return Task.Run(() => File.Delete(path));
            }
            return null;
        }

        public async Task WriteFileAsync(string fileName, TModel model)
        {
            string fullFilePath = this.databaseContext.GetFullFilePath(databaseOptions, fileName);
            if (File.Exists(fullFilePath))
            {
                try
                {
                    readerWriterLock.AcquireWriterLock(-1);
                    await WriteFileAsync(model, fullFilePath);
                }
                finally
                {
                    readerWriterLock.ReleaseWriterLock();
                }
            }
            else
               await WriteFileAsync(model, fullFilePath);
        }

        public IList<TModel> GetAllFiles()
        {
            return ReadAllExistingFilesAsync(fileProcessingHelper.GetPathToDestinationFolder(databaseOptions.DatabaseExtention, typeof(TModel).Name, databaseOptions.FolderName)).Result;
        }

        private async Task<IList<TModel>> ReadAllExistingFilesAsync(string databaseFolderPath)
        {
            var fileSet = new List<TModel>();
            if (Directory.Exists(databaseFolderPath))
            {
                foreach (string enumerateFile in Directory.EnumerateFiles(databaseFolderPath, "*" + databaseOptions.DatabaseExtention))
                {
                    using (StreamReader streamReader = new StreamReader(enumerateFile))
                    {
                        TModel model = await Task.Run(() => modelConverter.Deserialize(streamReader));
                        fileSet.Add(model);
                    }
                }
            }
            else
                CreateEmptyFolder(databaseFolderPath);
            return fileSet;
        }

        public async Task UpdateDatabaseFilesAsync(
          string fileName,
          TModel model,
          ModificationType modificationType)
        {
            switch (modificationType)
            {
                case ModificationType.UPDATE:
                    await WriteFileAsync(fileName, model);
                    break;
                case ModificationType.DELETE:
                   await DeleteFile(fileName);
                   break;
            }
        }

        public async Task CreateFileAsync(TModel fileModel)
        {
            string createdFileName = string.Empty;
            if (fileModel != null)
            {
                createdFileName = databaseContext.CreateEmptyFile(fileModel.Id, databaseOptions);
                await WriteFileAsync(fileModel, createdFileName);
            }
        }

        private async Task WriteFileAsync(TModel model, string fullPath)
        {
            using (StreamWriter streamWriter =  new StreamWriter(fullPath))
                await Task.Run(() => modelConverter.Serialize(streamWriter, model));
        }

        private void CreateEmptyFolder(string databaseFolderPath)
        {
            Directory.CreateDirectory(databaseFolderPath);
        }
    }
}