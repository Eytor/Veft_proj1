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
        // GET api/
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

        [Route("authors")]
        [HttpGet]
        public ActionResult<string> GetAllAuthors()
        {
            return Ok(_technicalRadiationService.GetAllAuthors());
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
