using System.Collections.Generic;
using DataStorageApplication.WebApi.DatabaseModels.GiftCards;
using DocumentDatabase.Extensibility.Service;
using Microsoft.AspNetCore.Mvc;

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
            return fileService.GetAll();
        }

        [HttpGet("{fileName}")]
        public GiftCardDto Get(string fileName)
        {
            return fileService.Get(fileName);
        }

        [HttpPost]
        public string Post([FromBody]GiftCardDto fileModel)
        {
            return fileService.Create(fileModel);
        }

        [HttpPut("{fileName}")]
        public void Put(string fileName, [FromBody]GiftCardDto category)
        {
            fileService.Update(fileName, category);
        }

        [HttpDelete("{fileName}")]
        public bool Delete(string fileName)
        {
            return fileService.Delete(fileName);
        }
    }
}
