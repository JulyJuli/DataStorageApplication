using DocumentDatabase.Extensibility.Helpers;
using System.Collections.Generic;
using DocumentDatabase.Extensibility.DTOs;
using System.Threading.Tasks;

namespace DocumentDatabase.Extensibility.Domain.Repository
{
    public interface IDocumentDatabaseRepository<TModel>
        where TModel : ModelIdentifier
    {
        Task DeleteFile(string fileName);

        Task UpdateDatabaseFilesAsync(string id, TModel model, ModificationType modificationType);

        IList<TModel> GetAllFiles();

        Task WriteFileAsync(string fileName, TModel model);

        Task CreateFileAsync(TModel fileModel);
    }
}
