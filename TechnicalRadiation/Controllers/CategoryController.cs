using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
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

        public IAuthentication Authentication { get; }
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
            var category =_categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }


        // Post api/categories
        [Route("")]
        [HttpPost]
        public IActionResult CreateNewCategory([FromBody] CategoryInputModel newCategory, [FromHeader]string Authorization)
        {
            if (Authentication.Authenticate(Authorization) == false)
            {
                return Unauthorized();
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
        public IActionResult UpdateAuthorById(int id, [FromBody] CategoryInputModel category, [FromHeader]string Authorization)
        {
            if (Authentication.Authenticate(Authorization) == false)
            {
                return Unauthorized();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not properly formatted");
            }

            // Return 404 if category is not found
            try
            {
                _categoryService.UpdateCategoryById(category, id);
            }
            catch (System.Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE api/categories/1
        [Route("{id:int}")]
        [HttpDelete]
        public ActionResult DeleteCategoryById(int id, [FromHeader]string Authorization)
        {
            if (Authentication.Authenticate(Authorization) == false)
            {
                return Unauthorized();
            }
            // Return 404 if category is not found
            try
            {
                _categoryService.DeleteCategoryById(id);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Post api/categories/1/newsItems/1
        [Route("{categoryId:int}/newsItems/{newsItemId:int}")]
        [HttpPost]
        public IActionResult LinkNewsToCategory(int categoryId, int newsItemId, [FromHeader]string Authorization)
        {
            if (Authentication.Authenticate(Authorization) == false)
            {
                return Unauthorized();
            }
            // Return 404 if category or news are not found
            try
            {
                _categoryService.LinkNewsItemToCategory(categoryId, newsItemId);
            }
            catch (System.Exception)
            {
                return NotFound();
            }

            return Ok();
        }

    }
}