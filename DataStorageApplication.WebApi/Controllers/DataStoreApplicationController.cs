using System.Collections.Generic;
using DocumentDatabase.Extensibility.DTOs;
using DocumentDatabase.Extensibility.DTOs.DatabaseModels.GiftCards;
using DocumentDatabase.Extensibility.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DataStorageApplication.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class DataStoreApplicationController: Controller
    {
        private readonly IDocumentDatabaseService<GiftCardDto> fileService;

        public DataStoreApplicationController(IDocumentDatabaseService<GiftCardDto> fileService)
        {
            this.fileService = fileService;
        }

        [HttpGet]
        public IList<GiftCardDto> GetAll()
        {
            return fileService.GetAllFiles();
        }

        [HttpGet("{fileName}")]
        public GiftCardDto Get(string fileName)
        {
            return fileService.GetFile(fileName);
        }


        [HttpPost]
        public string Post([FromBody]GiftCardDto fileModel)
        {
            return fileService.CreateFile(fileModel);
        }

        [HttpPut("{fileName}")]
        public void Put(string fileName, [FromBody]GiftCardDto category)
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
