using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Services;
using TechnicalRadiation.Models;
using Microsoft.Extensions.Configuration;
using TechnicalRadiation.Repositories;

namespace TechnicalRadiation.Controllers
{
    [Route("api")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public IConfiguration Config { get; }

        public IAuthentication Authentication { get; }
    
        private TechnicalRadiationService _technicalRadiationService = new TechnicalRadiationService();

        public ValuesController(IConfiguration config, IAuthentication Authentication)
        {
            Authentication = Authentication;
            Config = config;
        }

        // GET api
        [Route("")]
        [HttpGet]
        public IActionResult GetAllNews([FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            return Ok(_technicalRadiationService.GetAllNews(pageSize, pageNumber));
        }

        // GET api/5
        [Route("{id:int}")]
        [HttpGet]
        public ActionResult<string> GetNewsById(int id)
        {
            return Ok(_technicalRadiationService.GetNewsById(id));
        }

        // GET api/categories
        [Route("categories")]
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            return Ok(_technicalRadiationService.getAllCategories());
        }

        // GET api/categories/1
        [Route("categories/{id:int}")]
        [HttpGet]
        public ActionResult<string> GetCategoryById(int id)
        {
            return Ok(_technicalRadiationService.GetCategoryById(id));
        }

        // GET api/authors
        [Route("authors")]
        [HttpGet]
        public ActionResult<string> GetAllAuthors()
        {
            return Ok(_technicalRadiationService.GetAllAuthors());
        }

        // GET api/authors/1
        [Route("authors/{id:int}")]
        [HttpGet]
        public ActionResult<string> GetAuthorById(int id)
        {
            return Ok();
        }

        // GET api/authors/1/newsItems
        [Route("authors/{id:int}/newsItems")]
        [HttpGet]
        public ActionResult<string> GetAuthorsNewsItems(int id)
        {
            return Ok();
        }
        
        [HttpGet]
        [Route("/authors/{authorID}")]
        public ActionResult<string> Get([FromHeader]int id)
        {
            return Ok(_technicalRadiationService.GetAllAuthors(id));
        }

        [HttpGet]
        [Route("/authors/{authorID}/newsItem")]
        public ActionResult<string> Get([FromHeader]int authorID, int id)
        {

            return Ok(_technicalRadiationService.GetAllNewsItems(authorID, id));
            // return $"{authorID} Authenticated";
        }

        // The dotnet should have already new-ed a news file which I received from body.
        [HttpGet]
        [Route("/authors/{authorID}")]
        public ActionResult<string> Post([FromHeader] string xApiKey, [FromBody] TechnicalRadiation.Models.NewsItem news)
        {
            if (Authentication.Authenticate(xApiKey) == false)
            {
                return "Please provide authentication.";
            }
            // Take news and save in db if you have one
            // var news = new NewsItem();
            // Fill inn parameters
            // news.name = "";
            TechnicalRadiation.Models.NewsItem.save(news);
            // DBNull.save(news);
            return Created();
            // return $"{authorID} Authenticated";
        }

        [HttpGet]
        [Route("/authors/{authorID}/newsItem")]
        public ActionResult<string> Post([FromHeader]int id, [FromHeader]string xApiKey, [FromBody] TechnicalRadiation.Models.NewsItem news)
        {
            if (Authentication.Authenticate(xApiKey) == false)
            {
                return "Please provide authentication.";
            }

            return Created();
            // return $"{authorID} Authenticated";
        }



        // POST api/
        [Route("")]
        [HttpPost]
        public IActionResult CreateNewNewsItem([FromBody] string value)
        {
            return Ok();
        }

        // PUT api/5
        [HttpPut("{id:int}")]
        public IActionResult UpdateNewsItem(int id, [FromBody] string value)
        {
            return NoContent();
        }

        [Route("/{newsitemId}")] 
        [HttpDelete]
        public ActionResult Delete([FromHeader]int id, [FromHeader]string xApiKey)
        {
            var delId = _technicalRadiationService.GetNewsById(id);
            if(_technicalRadiationService.GetNewsById(id)){
                technicalRadiationService.Delete(id);
            }
        }


        [Route("/categories/{categoryId}")] 
        [HttpDelete]
        public ActionResult Delete([FromHeader]int cId, [FromHeader]string xApiKey)
        {
            var delId = _technicalRadiationService.GetNewsById(cId);
            if(_technicalRadiationService.GetNewsById(id)){
                technicalRadiationService.Delete(id);
            }
        }


        // POST api/categories
        [Route("categories")]
        [HttpPost]
        public IActionResult CreateNewCategory([FromBody] string value)
        {
            return Ok();
        }

        // PUT api/categories/5
        [HttpPut("categories/{id:int}")]
        public IActionResult UpdateCategory(int id, [FromBody] string value)
        {
            return NoContent();
        }

        // DELETE api/categories/5
        [HttpDelete("categories/{id:int}")]
        public IActionResult DeleteCategory(int id)
        {
            return Ok();
        }

        // POST api/authors
        [Route("authors")]
        [HttpPost]
        public IActionResult CreateNewAuthor([FromBody] string value)
        {
            return Ok();
        }

        // PUT api/authors/5
        [HttpPut("authors/{id:int}")]
        public IActionResult UpdateAuthor(int id, [FromBody] string value)
        {
            return NoContent();
        }

        // DELETE api/authors/5
        [HttpDelete("authors/{id:int}")]
        public IActionResult DeleteAuthor(int id)
        {
            return Ok();
        }

        // PUT api/authors/5/newsItems/10
        [HttpPut("authors/{authorId:int}/newsItems/{newsItemId:int}")]
        public IActionResult LinkAuthorToNews(int authorId, int newsItemId)
        {
            return NoContent();
        }
    }
}
