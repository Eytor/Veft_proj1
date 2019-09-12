using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Services;
using TechnicalRadiation.Services.Interfaces;

namespace TechnicalRadiation.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : Controller
    {
        public IConfiguration Config { get; }
        public IAuthorization Authorization { get; }

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService, IConfiguration config, IAuthentication authentication)
        {
            _categoryService = categoryService;
            Authentication = authentication;
            Config = config;
        }

         // GET api/categories
        [Route("")]
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            return Ok(_categoryService.GetAllCategories());
        }

        // GET api/categories/1
        [Route("{id:int}", Name = "GetCategoryById")]
        [HttpGet]
        public ActionResult<string> GetCategoryById(int id)
        {
            return Ok(_categoryService.GetCategoryById(id));
        }


         // Post api/categories
        [Route("")]
        [HttpPost]
        public IActionResult CreateNewCategory([FromBody] CategoryInputModel newCategory, [FromHeader]string Authorization)
        {
            if (Authentication.Authenticate(Authorization) == false)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not properly formatted");
            }
            int newId = _categoryService.CreateNewCategory(newCategory);

            return CreatedAtRoute("GetCategoryById", new { id = newId }, null);
        }


        // PUT api/categories/1
        [Route("{id:int}")]
        [HttpPut]
        public IActionResult UpdateAuthorById( int id, [FromBody] CategoryInputModel category, [FromHeader]string Authorization)
        {
            if (Authentication.Authenticate(Authorization) == false)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not properly formatted");
            }
            _categoryService.UpdateCategoryById(category, id);
            return NoContent();
        }

        // DELETE api/categories/1
        [Route("{id:int}")]
        [HttpDelete]
        public ActionResult DeleteCategoryById(int id, [FromHeader]string Authorization)
        {
            if (Authentication.Authenticate(Authorization) == false)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            _categoryService.DeleteCategoryById(id);
            return NoContent();
        }

         // Post api/categories/1/newsItems/1
        [Route("{categoryId:int}/newsItems/{newsItemId:int}")]
        [HttpPost]
        public IActionResult LinkNewsToCategory(int categoryId, int newsItemId)
        {
            _categoryService.LinkNewsItemToCategory(categoryId, newsItemId);
            return Ok();
        }

    }
}