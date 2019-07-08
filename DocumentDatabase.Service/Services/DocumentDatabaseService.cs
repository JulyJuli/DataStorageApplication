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
        private IDictionary<string, TModel> databaseFiles;

        public DocumentDatabaseService(
          IDocumentDatabaseRepository<TModel> fileRepository,
          IFileProcessingHelper fileProcessingHelper)
        {
            this.fileRepository = fileRepository;
            this.fileProcessingHelper = fileProcessingHelper;
            databaseFiles = new Dictionary<string, TModel>();
        }

        public TModel GetFile(string fileName)
        {
            TModel model;
            if (databaseFiles.TryGetValue(fileName, out model))
                return model;
            return default(TModel);
        }

        public string CreateFile(TModel fileModel)
        {
            string file = fileRepository.CreateFile(fileModel);
            if (fileModel != null)
                databaseFiles.Add(file, fileModel);
            return file;
        }

        public bool DeleteFile(string fileName)
        {
            TModel model;
            databaseFiles.TryGetValue(fileName, out model);
            if (model == null || !this.fileRepository.UpdateDatabaseFiles(fileName, model, ModificationType.DELETE))
                return false;
            databaseFiles.Remove(fileName);
            return true;
        }

        public bool UpdateFile(string fileName, TModel model)
        {
            TModel fileModel;
            databaseFiles.TryGetValue(fileName, out fileModel);
            if (fileModel == null)
                return false;
            databaseFiles[fileName] = model;
            fileRepository.WriteFile(fileName, model);
            return true;
        }

        public IList<TModel> GetAllFiles(DatabaseOptions databaseOptions)
        {
            List<TModel> modelList = new List<TModel>();
            databaseFiles = this.fileRepository.GetAllFiles(databaseOptions);
            foreach (TModel model in this.databaseFiles.Values)
                modelList.Add(model);
            return modelList;
        }
    }
}
