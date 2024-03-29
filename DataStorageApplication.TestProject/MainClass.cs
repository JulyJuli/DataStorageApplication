﻿using DocumentDatabase.Extensibility.DTOs;
using DocumentDatabase.Extensibility.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Text.RegularExpressions;
using DataStorageApplication.TestProject.DatabaseModels.TodoListModel;
using DocumentDatabase.Module;

namespace DataStorageApplication.TestProject
{
    public class MainClass
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            try
            {
                ConfigureServices(serviceCollection);
                var serviceProvider = serviceCollection.BuildServiceProvider();

                var databaseConfiguration = serviceProvider.GetService<IDocumentDatabaseService<Task>>();

                var firstModel = databaseConfiguration.Create(new Task("first task", "test description", false, false, new DateTime().Date, "comment", null));
                var allFiles = databaseConfiguration.GetAll();
            }
            catch(InvalidDataException exception) {
                Console.WriteLine(exception.Message);
                return;
            }
            Console.ReadKey();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // build configuration
            var configuration = new ConfigurationBuilder()
              .SetBasePath(GetApplicationRoot())
              .AddJsonFile("appsettings.json", false)
              .AddEnvironmentVariables()
              .Build();

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
