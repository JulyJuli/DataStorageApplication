using DocumentDatabase.Domain;
using DocumentDatabase.Domain.Repository;
using DocumentDatabase.Extensibility.Converters.ModelConverters;
using DocumentDatabase.Extensibility.Domain;
using DocumentDatabase.Extensibility.Domain.Repository;
using DocumentDatabase.Extensibility.Factories;
using DocumentDatabase.Extensibility.Helpers;
using DocumentDatabase.Extensibility.Service;
using DocumentDatabase.Service.Converters;
using DocumentDatabase.Service.Factories;
using DocumentDatabase.Service.Helpers;
using DocumentDatabase.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DataStorageApplication.Starup
{
    public static class DocumentDatabaseStartup
    {
        public static IServiceCollection ConfigureDocumentDatabase(this IServiceCollection services)
        {
            return services
                    .AddTransient(typeof(IDocumentDatabaseRepository<>), typeof(DocumentDatabaseRepository<>))
                    .AddSingleton(typeof(IDatabaseContext<>), typeof(DatabaseContext<>))
                    .AddSingleton(typeof(IFileExtentionFactoryRetriever<>),typeof(FileExtentionFactoryRetriever<>))
                    .AddSingleton(typeof(IDocumentDatabaseService<>), typeof(DocumentDatabaseService<>))
                    .AddSingleton(typeof(IJsonConverter<>), typeof(JsonConverter<>))
                    .AddSingleton(typeof(IXmlConverter<>), typeof(XmlConverter<>))
                    .AddSingleton<IFileProcessingHelper, FileProcessingHelper>();
        }
    }
}