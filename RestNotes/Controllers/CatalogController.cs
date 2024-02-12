using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RestNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CatalogController : ControllerBase
    {
        private readonly IRepositoryPathCatalog _repository;

        public CatalogController(
            IRepositoryPathCatalog repository
        )
        {
            _repository = repository;
        }

        [HttpGet("get-category-content")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategoryContent([FromQuery] CatalogContentQueryDTO dto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException();
            PageDTO<CatalogContentItemDTO> result = await _repository.GetContent(
                new UserDataDTO<CatalogContentQueryDTO> (dto, userId),
                HttpContext.RequestAborted
            );

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CatalogInsertDTO dto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException();
            await _repository.Insert(
                new UserDataDTO<CatalogInsertDTO>(dto, userId),
                HttpContext.RequestAborted
            );

            return Ok();
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] CatalogUpdateDTO dto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException();
            await _repository.Update(
                new UserDataDTO<CatalogUpdateDTO>(dto, userId),
                HttpContext.RequestAborted);

            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromBody] CatalogItemDTO dto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException();
            await _repository.Delete(
                new UserDataDTO<CatalogItemDTO>(dto, userId),
                HttpContext.RequestAborted
            );

            return Ok();
        }
    }
}
