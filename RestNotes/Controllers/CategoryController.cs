using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestNodes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public CategoryController() 
        {
        }

        [HttpGet("get-category-content")]
        public async Task<IActionResult> GetCategoryContent()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        public async Task<IActionResult> Update()
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            throw new NotImplementedException();
        }
    }
}
