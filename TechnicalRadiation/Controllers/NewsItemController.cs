using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Services;
using TechnicalRadiation.Services.Interfaces;

namespace TechnicalRadiation.Controllers
{
    [Route("api")]
    [ApiController]
    public class NewsItemController : ControllerBase
    {
        public IConfiguration Config { get; }

        public IAuthentication Authentication { get; }
        private readonly INewsItemService _newsItemService;

        public NewsItemController(INewsItemService newsItemService, IConfiguration config, IAuthentication authentication)
        {
            _newsItemService = newsItemService;
            Authentication = authentication;
            Config = config;
        }

        // GET api
        [Route("")]
        [HttpGet]
        public IActionResult GetAllNews([FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            return Ok(_newsItemService.GetAllNewsItems(pageSize, pageNumber));
        }

        [Route("{id:int}", Name = "GetNewsById")]
        [HttpGet]
        public ActionResult<string> GetNewsById(int id)
        {
            return Ok(_newsItemService.GetNewsById(id));
        }

        // POST api/
        [Route("")]
        [HttpPost]
        public IActionResult CreateNewNewsItem([FromBody] NewsItemInputModel newsItem)
        {
            int newId = _newsItemService.CreateNewNewsItem(newsItem);
            return CreatedAtRoute("GetNewsById", new { id = newId }, null);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IActionResult UpdateNewsItemById(int id, [FromBody]NewsItemInputModel news )
        {
            if(!ModelState.IsValid){
            return BadRequest("Model is not properly formatted");
           }
            _newsItemService.UpdateNewsItemById(news, id);
            return NoContent();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public ActionResult Delete([FromHeader]int id, [FromHeader]string xApiKey)
        {
            _newsItemService.DeleteNewsById(id);
            return NoContent();
        }

    }
}