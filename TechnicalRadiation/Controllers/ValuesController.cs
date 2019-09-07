using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Services;

namespace TechnicalRadiation.Controllers
{
    [Route("api")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private TechnicalRadiationService _technicalRadiationService = new TechnicalRadiationService();

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

        // DELETE api/5
        [HttpDelete("{id:int}")]
        public IActionResult DeleteNewsItem(int id)
        {
            return Ok();
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
