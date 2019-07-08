using System.Collections.Generic;
using DocumentDatabase.Extensibility.DTOs;
using DocumentDatabase.Extensibility.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ToDoList.Extensibility.Models;

namespace DataStorageApplication.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class DataStoreApplicationController : Controller
    {
        private readonly DatabaseOptions databaseOptions;
        private readonly IDocumentDatabaseService<Category> fileService;

        public DataStoreApplicationController(IOptions<DatabaseOptions> databaseOptions, IDocumentDatabaseService<Category> fileService)
        {
            this.fileService = fileService;
            this.databaseOptions = databaseOptions.Value;
        }

        [HttpGet]
        public IList<Category> GetAll()
        {
            return fileService.GetAllFiles(databaseOptions);
        }

        [HttpGet("{fileName}")]
        public Category Get(string fileName)
        {
            return fileService.GetFile(fileName);
        }


        [HttpPost]
        public string Post([FromBody]Category fileModel)
        {
            return fileService.CreateFile(fileModel);
        }

        [HttpPut("{fileName}")]
        public void Put(string fileName, [FromBody]Category category)
        {
            fileService.UpdateFile(fileName, category);
        }

        [HttpDelete("{fileName}")]
        public bool Delete(string fileName)
        {
            return fileService.DeleteFile(fileName);
        }
    }
}
