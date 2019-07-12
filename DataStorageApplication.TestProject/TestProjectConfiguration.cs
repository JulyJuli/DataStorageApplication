using DocumentDatabase.Extensibility.DatabaseModels;
using DocumentDatabase.Extensibility.DTOs;
using DocumentDatabase.Extensibility.Service;
using Microsoft.Extensions.Options;

namespace DataStorageApplication.TestProject
{
   public class TestProjectConfiguration<TModel>
        where TModel : ModelIdentifier
    {
        public TestProjectConfiguration(IOptions<DatabaseOptions> databaseOptions, IDocumentDatabaseService<TModel> fileService)
        {
            FileService = fileService;
            DatabaseOptions = databaseOptions.Value;
        }

        public DatabaseOptions DatabaseOptions { get; }

        public IDocumentDatabaseService<TModel> FileService { get; }
    }
}