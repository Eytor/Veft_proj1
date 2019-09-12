using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Services;
using TechnicalRadiation.Services.Interfaces;

namespace TechnicalRadiation.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        public IConfiguration Config { get; }

        public IAuthentication Authentication { get; }
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService, IConfiguration config, IAuthentication authentication)
        {
            _authorService = authorService;
            Authentication = authentication;
            Config = config;
        }

         // GET api/authors
        [Route("")]
        [HttpGet]
        public IActionResult GetAllAuthors([FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            return Ok(_authorService.GetAllAuthors());
        }

        // GET api/authors/1
        [Route("{id:int}", Name = "GetAuthorById")]
        [HttpGet]
        public ActionResult<string> GetAuthorById(int id)
        {
            return Ok(_authorService.GetAuthorById(id));
        }

        // GET api/authors/1/newsItems
        [Route("{id:int}/newsItems")]
        [HttpGet]
        public ActionResult<string> GetNewsItemsByAuthor(int id)
        {
            return Ok(_authorService.GetNewsByAuthorId(id));
        }

         // Post api/authors
        [Route("")]
        [HttpPost]
        public IActionResult CreateNewAuthor([FromBody] AuthorInputModel newAuthor, [FromHeader]string xApiKey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not properly formatted");
            }
            int newId = _authorService.CreateNewAuthor(newAuthor);

            return CreatedAtRoute("GetAuthorById", new { id = newId }, null);
        }


        // PUT api/authors/1
        [Route("{id:int}")]
        [HttpPut]
        public IActionResult UpdateAuthorById( int id, [FromBody] AuthorInputModel newAuthor, [FromHeader]string xApiKey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not properly formatted");
            }
            _authorService.UpdateAuthorById(newAuthor, id);
            return NoContent();
        }

        // DELETE api/authors/1
        [Route("{id:int}")]
        [HttpDelete]
        public ActionResult DeleteAuthorById([FromHeader]int id, [FromHeader]string xApiKey)
        {
            _authorService.DeleteAuthorById(id);
            return NoContent();
        }

         // Post api/authors/1/newsItems/1
        [Route("{authorId:int}/newsItems/{newsItemId:int}")]
        [HttpPost]
        public IActionResult CreateNewAuthor(int authorId, int newsItemId)
        {
            _authorService.LinkAuthorToNewsItem(authorId, newsItemId);
            return Ok();
        }
    }
}