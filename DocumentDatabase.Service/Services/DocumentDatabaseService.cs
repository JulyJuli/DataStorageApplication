using DocumentDatabase.Extensibility.DatabaseModels;
using DocumentDatabase.Extensibility.Domain.Repository;
using DocumentDatabase.Extensibility.DTOs;
using DocumentDatabase.Extensibility.Helpers;
using DocumentDatabase.Extensibility.Service;
using System.Collections.Generic;

namespace DocumentDatabase.Service.Services
{
    public class DocumentDatabaseService<TModel> : IDocumentDatabaseService<TModel>
        where TModel : ModelIdentifier
    {
        private readonly IDocumentDatabaseRepository<TModel> fileRepository;
        private readonly IFileProcessingHelper fileProcessingHelper;
        private IList<TModel> databaseFiles;

        public DocumentDatabaseService(
          IDocumentDatabaseRepository<TModel> fileRepository,
          IFileProcessingHelper fileProcessingHelper)
        {
            this.fileRepository = fileRepository;
            this.fileProcessingHelper = fileProcessingHelper;
            databaseFiles = new List<TModel>();
        }

        public TModel GetFile(string fileName)
        {
            return GetDatabaseFile(fileName);
        }

        public string CreateFile(TModel fileModel)
        {
            string filePath = fileRepository.CreateFile(fileModel);
            if (fileModel != null && !string.IsNullOrEmpty(filePath))
                databaseFiles.Add(fileModel);
            return filePath;
        }

        public bool DeleteFile(string fileName)
        {
            TModel model = GetDatabaseFile(fileName);
            if (model == null || !this.fileRepository.UpdateDatabaseFiles(fileName, model, ModificationType.DELETE))
                return false;

            databaseFiles.Remove(model);
            return true;
        }

        public bool UpdateFile(string fileName, TModel model)
        {
            TModel existingModel = GetDatabaseFile(fileName);
            if (existingModel == null || model == null)
                return false;

            fileRepository.WriteFile(fileName, model);
            databaseFiles.Add(model);
            return true;
        }

        public IList<TModel> GetAllFiles(DatabaseOptions databaseOptions)
        {
            this.databaseFiles = fileRepository.GetAllFiles(databaseOptions);
            return databaseFiles;
        }

        private TModel GetDatabaseFile(string fileName) {
            foreach (var file in databaseFiles)
            {
                if (file.Id == fileName)
                    return file;
            }
            return null;
        }
    }
}
