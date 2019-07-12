using DataStorageApplication.Module;
using DocumentDatabase.Extensibility.DatabaseModels.TodoListModel;
using DocumentDatabase.Extensibility.DTOs;
using DocumentDatabase.Extensibility.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace DataStorageApplication.TestProject
{
    public class MainClass
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var databaseConfiguration = serviceProvider.GetService<IDocumentDatabaseService<Task>>();

            var firstModel = databaseConfiguration.CreateFile(new Task ("first task", "test description", false, false, new DateTime().Date,"comment"));
            var allFiles = databaseConfiguration.GetAllFiles();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // build configuration
            var configuration = new ConfigurationBuilder()
              .SetBasePath(GetApplicationRoot())
              .AddJsonFile("appsettings.json", false)
              .AddEnvironmentVariables()
              .Build();

            serviceCollection.AddOptions();
            serviceCollection.Configure<DatabaseOptions>(configuration.GetSection("DatabaseConfiguration"));
            serviceCollection.ConfigureDocumentDatabase();
        }

        private static string GetApplicationRoot()
        {
            var exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            var appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;

            return appRoot;
        }
    }
}
