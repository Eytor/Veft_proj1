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
        public IAuthorization Authorization { get; }

        private readonly INewsItemService _newsItemService;

        public NewsItemController(INewsItemService newsItemService, IConfiguration config, IAuthorization authorization)
        {
            _newsItemService = newsItemService;
            Authorization = authorization;
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
        public IActionResult CreateNewNewsItem([FromBody] NewsItemInputModel newsItem, [FromHeader]string Authorization)
        {
            if (Authentication.Authenticate(Authorization) == false)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not properly formatted");
            }
            int newId = _newsItemService.CreateNewNewsItem(newsItem);
            return CreatedAtRoute("GetNewsById", new { id = newId }, null);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IActionResult UpdateNewsItemById(int id, [FromBody]NewsItemInputModel news, [FromHeader]string Authorization)
        {
            if (Authentication.Authenticate(Authorization) == false)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not properly formatted");
            }
            _newsItemService.UpdateNewsItemById(news, id);
            return NoContent();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public ActionResult Delete(int id, [FromHeader]string Authorization)
        {
            if (Authentication.Authenticate(Authorization) == false)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            _newsItemService.DeleteNewsById(id);
            return NoContent();
        }

    }
}