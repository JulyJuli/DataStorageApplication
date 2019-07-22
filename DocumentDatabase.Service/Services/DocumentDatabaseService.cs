using DocumentDatabase.Extensibility.Domain.Repository;
using DocumentDatabase.Extensibility.Helpers;
using DocumentDatabase.Extensibility.Service;
using System.Collections.Generic;
using System.Linq;
using DocumentDatabase.Extensibility.DTOs;
using System.IO;

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
            databaseFiles = fileRepository.GetAllFiles();
        }

        public TModel Get(string fileName)
        {
            return GetDatabaseFile(fileName);
        }

        public string Create(TModel fileModel)
        {
            if (fileModel == null)
            {
                throw new InvalidDataException("Input model is empty. Please check source data.");
            }
            else {
                databaseFiles.Add(fileModel);
                fileRepository.CreateFileAsync(fileModel);
                return fileModel.Id;
            }       
        }

        public bool Delete(string fileName)
        {
            TModel model = GetDatabaseFile(fileName);
            if (model != null)
            {
                databaseFiles.Remove(model);
                fileRepository.UpdateDatabaseFilesAsync(fileName, model, ModificationType.DELETE);
                return true;
            }
            return false;
        }

        public bool Update(string fileName, TModel model)
        {
            TModel existingModel = GetDatabaseFile(fileName);
            if (existingModel == null || model == null)
                return false;
            else
            {
                databaseFiles.Remove(existingModel);
                databaseFiles.Add(model);

                fileRepository.WriteFileAsync(fileName, model);
                return true;
            }
        }

        public IList<TModel> GetAll()
        {
            return databaseFiles;
        }

        private TModel GetDatabaseFile(string fileName) {
           var foundFile = databaseFiles.FirstOrDefault(file => file.Id == fileName);
            if (foundFile == null)
            {
                throw new FileNotFoundException("requested file not found.");
            }
            else
                return foundFile;
        }
    }
}
