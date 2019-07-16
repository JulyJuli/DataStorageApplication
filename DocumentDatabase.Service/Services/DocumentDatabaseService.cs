using DocumentDatabase.Extensibility.Domain.Repository;
using DocumentDatabase.Extensibility.Helpers;
using DocumentDatabase.Extensibility.Service;
using System.Collections.Generic;
using System.Linq;
using DocumentDatabase.Extensibility.DTOs;

namespace DocumentDatabase.Service.Services
{
    public class DocumentDatabaseService<TModel> : IDocumentDatabaseService<TModel>
        where TModel : ModelIdentifier
    {
        private readonly IDocumentDatabaseRepository<TModel> fileRepository;

        private IList<TModel> databaseFiles;

        public DocumentDatabaseService(IDocumentDatabaseRepository<TModel> fileRepository)
        {
            this.fileRepository = fileRepository;
            databaseFiles = new List<TModel>();
        }

        public TModel Get(string fileName)
        {
            return GetDatabaseFile(fileName);
        }

        public string Create(TModel fileModel)
        {
            string filePath = fileRepository.CreateFile(fileModel);
            if (fileModel != null && !string.IsNullOrEmpty(filePath))
                databaseFiles.Add(fileModel);
            return filePath;
        }

        public bool Delete(string fileName)
        {
            TModel model = GetDatabaseFile(fileName);
            if (model == null || !this.fileRepository.UpdateDatabaseFiles(fileName, model, ModificationType.DELETE))
                return false;

            databaseFiles.Remove(model);
            return true;
        }

        public bool Update(string fileName, TModel model)
        {
            TModel existingModel = GetDatabaseFile(fileName);
            if (existingModel == null || model == null)
                return false;

            fileRepository.WriteFile(fileName, model);
            databaseFiles.Add(model);
            return true;
        }

        public IList<TModel> GetAll()
        {
            this.databaseFiles = fileRepository.GetAllFiles();
            return databaseFiles;
        }

        private TModel GetDatabaseFile(string fileName) {
           return databaseFiles.FirstOrDefault(file => file.Id == fileName);
        }
    }
}
