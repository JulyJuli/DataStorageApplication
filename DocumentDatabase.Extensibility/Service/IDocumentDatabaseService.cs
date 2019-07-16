using System.Collections.Generic;
using DocumentDatabase.Extensibility.DTOs;

namespace DocumentDatabase.Extensibility.Service
{
    public interface IDocumentDatabaseService<TModel>
        where TModel : ModelIdentifier
    {
        IList<TModel> GetAll();
        TModel Get(string fileName);
        string Create(TModel file);
        bool Update(string fileName, TModel file);
        bool Delete(string fileName);
    }
}