using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentDatabase.WebApi
{
    public static class DocumentDatabaseStartup
    {
        public static IServiceCollection ConfigureDocumentDatabase(this IServiceCollection services)
        {
            return services
                    .AddTransient<IFileRepository, FileRepository>()
                    .AddSingleton<IDataStoreApplicationContext, DataStoreApplicationContext>()
                    .AddSingleton<IFileExtentionFactoryRetriever, FileExtentionFactoryRetriever>()
                    .AddSingleton<IFileService, FileService>()
                    .AddSingleton<IJsonConverter, JsonConverter>()
                    .AddSingleton<IByteConverter, ByteConverter>()
                    .AddSingleton<IXmlConverter, XmlConverter>()
                    .AddSingleton<IFileProcessingHelper, FileProcessingHelper>();
        }
    }
}
