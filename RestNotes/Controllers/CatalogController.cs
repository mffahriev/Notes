using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RestNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IReaderCatalogContent _readerCatalogContent;
        private readonly IRepository<Catalog> _repository;

        public CatalogController(
            IReaderCatalogContent readerCatalogContent,
            IRepository<Catalog> repository
        ) 
        {
            _readerCatalogContent = readerCatalogContent;
            _repository = repository;
        }

        [HttpGet("get-category-content")]
        public async Task<IActionResult> GetCategoryContent([FromQuery] CatalogContentQueryDTO dto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException();
            PageDTO<CatalogContentItemDTO> result = await _readerCatalogContent.GetCatalogContent(
                dto, 
                userId, 
                HttpContext.RequestAborted
            );

            return Ok(result);
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
